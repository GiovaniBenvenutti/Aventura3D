using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;
//using NaughtyAttributes;
using GGM.Cloth;

public class Player : Singleton<Player>
{
    public LoadSceneHelper loadSceneHelper;

    public List<Collider> colliders;
    PlayerStateManager playerStateManager;

    [Tooltip("CharacteController é o jeito certo de movimentar o player")]
    public CharacterController characterController;
    public Animator animator;
    public FSM_Player fsmPlayer;


    [Header("Moviment Setup")] public float speed = 1f;
    [Header("Moviment Setup")] public float turnSpeed = 1f;

    [Header("Animator Setup")] public AnimatorManager animatorManager;

    [Header("Jump Setup")] public float jumpForce = 20.0f;
    [Header("Jump Setup")] public bool isGrounded;
    [Header("Jump Setup")] public KeyCode jumpKey = KeyCode.Space;
    [Header("Jump Setup")] public float gravity = 20f;
    [Header("Jump Setup")] public float _vSpeed = 0f;
    //[Foldout("Jump Setup")] public bool isJumping = false;

    [Header("Run Setup")] public KeyCode runKey = KeyCode.LeftShift;
    [Header("Run Setup")] public float speedRun = 1.5f;
    [Header("Run Setup")] public Vector3 speedVector;

    [Header("FlashColor Setup")] public List<FlashColor3D> flashColors;

    [Header("LIfe Setup")] public bool isDead = false;
    [Header("LIfe Setup")] public HealthBase health;
    [Header("LIfe Setup")] public float damageCooldown = 1f; // intervalo em segundos
    [Header("LIfe Setup")] public Vector3 respawnOffSet = new Vector3(1,5,1);
    [Header("LIfe Setup")] public ScreenShake shake;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private float lastDamageTime = -Mathf.Infinity;
    //public UiGunUpdater uiGunUpdater;
    //[Space]
    


    // void OnValidate()
    // {
    // }

    protected override void Awake()
    {
        base.Awake();
        //OnValidate();   // garante que as referências estejam setadas mesmo no editor
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();
        if (animatorManager == null) animatorManager = GetComponent<AnimatorManager>();
        if (health == null) health = GetComponent<HealthBase>();
        if (shake == null) shake = GetComponent<ScreenShake>();
        playerStateManager = GetComponent<PlayerStateManager>();
        characterController = GetComponent<CharacterController>();
        if (loadSceneHelper == null) loadSceneHelper = FindObjectOfType<LoadSceneHelper>();

        //_clothChanger = GetComponent<ClothChanger>();

        health.OnDamage += Damage;
        health.OnKill += OnKill;
    }

    void Start()
    {
    //  ISSO É IMPORTANTE PRA ELE DETECTAR O CHÃO    
        characterController.minMoveDistance = 0.0f;
        characterController.skinWidth = 0.0001f;
        //fsmPlayer = new FSM_Player(this);
        SaveManager.Instance.GetLastCheckPoint();
        Invoke(nameof(Spawn), 0.1f);
    }

    void Update()
    {
        if (isDead) return; // se está morto, não processa mais nada

        // Rotação
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        float inputAxisVertical = Input.GetAxis("Vertical");
        speedVector = transform.forward * inputAxisVertical * speed;

        // Controle de grounded
        if (characterController.isGrounded && _vSpeed < 0)
        {
            _vSpeed = -1f; // mantém colado no chão
            if (Input.GetKeyDown(jumpKey))
            {
                playerStateManager.SwitchState(playerStateManager.jumpState);
                //animator.SetTrigger("jump");
                //_vSpeed = jumpForce;
            }
        }

        // Controle de corrida
        if (Input.GetKey(runKey) && isGrounded)
        {
            playerStateManager.SwitchState(playerStateManager.runState);
        }

        // aplica gravidade sempre
        _vSpeed -= gravity * Time.deltaTime;

        if (playerStateManager.currentState != playerStateManager.runState || isDead)
        {
            speedVector = transform.forward * inputAxisVertical * speed;
            speedVector.y = _vSpeed;
            characterController.Move(speedVector * Time.deltaTime);
        }

        // animação
        animator.SetBool("isRunning", inputAxisVertical != 0);
    }

    #region Life
        public void Damage(HealthBase healthBase)
        {
            flashColors.ForEach(f => f.Flash());
            EffectsManager.Instance.ChangeVignette();
            shake.ShakeTeste();
           // SaveManager.Instance.SavePlayerHealth(health._currentLife);
        }

        public void Damage(float damage, Vector3 direction)
        {
            //throw new NotImplementedException();
            //flashColors.ForEach(f => f.Flash());
            //Damage(damage);
            //health.Damage(damage);
        }

        public void OnKill(HealthBase healthBase)
        {
            if(!isDead)
            {
       //         Debug.Log("Morreu");
                isDead = true;
                playerStateManager.SwitchState(playerStateManager.dieState);
                //animator.SetTrigger("dead");
                colliders.ForEach(col => col.enabled = false);

               Invoke(nameof(Revive), 3f);
            }
        }

        public void Revive()
        {
            Respawn();
            health.ResetLife();
            isDead = false;
            animator.SetTrigger("idle");
            Invoke(nameof(TurnOnColliders), 0.1f);
        }

        private void TurnOnColliders()
        {
            colliders.ForEach(col => col.enabled = true);            
        }

    
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                if (Time.time - lastDamageTime >= damageCooldown)
                {
                    health.Damage(1f);
                    lastDamageTime = Time.time; // atualiza o momento do último dano
                }
                
            }
        }

    #endregion

    public void Spawn()
    {
        if(CheckPointManager.Instance.hasCheckPoint())
        {
            health._currentLife = SaveManager.Instance.GetHealthValue();
            Vector3 replace = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            replace += respawnOffSet;
            transform.position = replace;
            playerStateManager.SwitchState(playerStateManager.idleState);
        }
        else
        {
            Debug.Log("check point não encontrado em player.spaw");
        }
    }

    public void Respawn()
    {
        if(CheckPointManager.Instance.hasCheckPoint())
        {
         //   Vector3 replace = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
         //   replace += respawnOffSet;
         //   transform.position = replace;
         //   playerStateManager.SwitchState(playerStateManager.idleState);
            SaveManager.Instance.GetLastCheckPoint();
            health._currentLife = SaveManager.Instance.GetHealthValue();

            loadSceneHelper.Load(0);    // manda devolta pro menu
        }
    }


    #region ClothChanger
        public void ChangeSpeed(float newSpeed, float duration)
        {
            StartCoroutine(ChangeSpeedCoroutine(newSpeed, duration));
        }

        private IEnumerator ChangeSpeedCoroutine(float newSpeed, float duration)
        {
            float originalSpeed = speed;
            speed = newSpeed;

            yield return new WaitForSeconds(duration);

            speed = originalSpeed;
        }

        public void ChangeTexture(ClothSetup setup, float duration)
        {
            StartCoroutine(ChangeTextureCoroutine(setup, duration));
        }

        private IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
        {
            _clothChanger.ChangeTexture(setup);

            yield return new WaitForSeconds(duration);

            _clothChanger.ResetTexture();
        }

        public void ChangeSize(Vector3 scaleMultiplier, float duration)
        {
            StartCoroutine(ChangeSizeCoroutine(scaleMultiplier, duration));
        }

        private IEnumerator ChangeSizeCoroutine(Vector3 scaleMultiplier, float duration)
        {
            Vector3 originalScale = transform.localScale;
            transform.localScale = Vector3.Scale(originalScale, scaleMultiplier);

            yield return new WaitForSeconds(duration);

            transform.localScale = originalScale;
        }
    #endregion

}
