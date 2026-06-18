using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Collider> colliders;
    PlayerStateManager playerStateManager;
    public CharacterController characterController;

    public Animator animator;
    public float speed = 1f;
    public float turnSpeed = 1f;


    [Header("Animation Setup")]
    public AnimatorManager animatorManager;


    [Header("State Machine")]
    public FSM_Player fsmPlayer;
    public float jumpForce = 20.0f;
    public bool isGrounded;

    public KeyCode jumpKey = KeyCode.Space;
    public float gravity = 20f;

    public float _vSpeed = 0f;


    [Header("RunSetup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    public Vector3 speedVector;

    [Header("FlashColor")]
    public List<FlashColor3D> flashColors;


    [Header("Life")]
    public bool isDead = false;
    public HealthBase health;
    //public UiGunUpdater uiGunUpdater;
    public float damageCooldown = 1f; // intervalo em segundos
    private float lastDamageTime = -Mathf.Infinity;

    [Space]
    public Vector3 respawnOffSet = new Vector3(1,0,1);



    void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();
        if (animatorManager == null) animatorManager = GetComponent<AnimatorManager>();
        if (health == null) health = GetComponent<HealthBase>();
    }

    void Awake()
    {
        OnValidate();   // garante que as referências estejam setadas mesmo no editor
        playerStateManager = GetComponent<PlayerStateManager>();
        characterController = GetComponent<CharacterController>();
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
            Invoke(nameof(turnOnColliders), 0.1f);
        }

        private void turnOnColliders()
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

}
