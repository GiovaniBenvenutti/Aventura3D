using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;
using NaughtyAttributes;
using GGM.Cloth;

public class Player : Singleton<Player>
{
    public List<Collider> colliders;
    PlayerStateManager playerStateManager;

    [Tooltip("CharacteController é o jeito certo de movimentar o player")]
    public CharacterController characterController;
    public Animator animator;
    public FSM_Player fsmPlayer;

    [Foldout("Moviment Setup")] public float speed = 1f;
    [Foldout("Moviment Setup")] public float turnSpeed = 1f;

    [Foldout("Animator Setup")] public AnimatorManager animatorManager;

    [Foldout("Jump Setup")] public float jumpForce = 20.0f;
    [Foldout("Jump Setup")] public bool isGrounded;
    [Foldout("Jump Setup")] public KeyCode jumpKey = KeyCode.Space;
    [Foldout("Jump Setup")] public float gravity = 20f;
    [Foldout("Jump Setup")] public float _vSpeed = 0f;

    [Foldout("Run Setup")] public KeyCode runKey = KeyCode.LeftShift;
    [Foldout("Run Setup")] public float speedRun = 1.5f;
    [Foldout("Run Setup")] public Vector3 speedVector;

    [Foldout("FlashColor Setup")] public List<FlashColor3D> flashColors;

    [Foldout("LIfe Setup")] public bool isDead = false;
    [Foldout("LIfe Setup")] public HealthBase health;
    [Foldout("LIfe Setup")] public float damageCooldown = 1f; // intervalo em segundos
    [Foldout("LIfe Setup")] public Vector3 respawnOffSet = new Vector3(1,0,1);
    [Foldout("LIfe Setup")] public ScreenShake shake;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private float lastDamageTime = -Mathf.Infinity;
    //public UiGunUpdater uiGunUpdater;
    //[Space]
    


    void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();
        if (animatorManager == null) animatorManager = GetComponent<AnimatorManager>();
        if (health == null) health = GetComponent<HealthBase>();
        if (shake == null) shake = GetComponent<ScreenShake>();
    }

    protected override void Awake()
    {
        base.Awake();
        //OnValidate();   // garante que as referências estejam setadas mesmo no editor
        playerStateManager = GetComponent<PlayerStateManager>();
        characterController = GetComponent<CharacterController>();
        //_clothChanger = GetComponent<ClothChanger>();
        //animator = GetComponent<Animator>();
        //animatorManager = GetComponent<AnimatorManager>();

        health.OnDamage += Damage;
        health.OnKill += OnKill;
    }

    void Start()
    {
    //  ISSO É IMPORTANTE PRA ELE DETECTAR O CHÃO    
        characterController.minMoveDistance = 0.0f;
        characterController.skinWidth = 0.0001f;
        //fsmPlayer = new FSM_Player(this);
    }

    void Update()
    {
        if (isDead)
        {
            // se está morto, não processa mais nada
            return;
        }
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
                Debug.Log("Morreu");
                isDead = true;
                playerStateManager.SwitchState(playerStateManager.dieState);
                //animator.SetTrigger("dead");
                colliders.ForEach(col => col.enabled = false);

                Invoke(nameof(Revive), 4f);
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


    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckPointManager.Instance.hasCheckPoint())
        {
            Vector3 replace = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            replace += respawnOffSet;
            transform.position = replace;
            playerStateManager.SwitchState(playerStateManager.idleState);

        }
    }


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


}
