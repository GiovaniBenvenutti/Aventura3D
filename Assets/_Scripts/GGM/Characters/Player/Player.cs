using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    public bool isDead = false;

    public KeyCode jumpKey = KeyCode.Space;
    public float gravity = 20f;

    public float _vSpeed = 0f;


    [Header("RunSetup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    public Vector3 speedVector;

    void Awake()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        //animatorManager = GetComponent<AnimatorManager>();
    }

    void Start()
    {
    //  ISSO É IMPORTANTE PRA ELE DETECTAR O CHÃO    
        characterController.minMoveDistance = 0.0f;
        characterController.skinWidth = 0.0001f;
        //fsmPlayer = new FSM_Player(this);
    }



    // Update is called once per frame
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            // marca como morto
            isDead = true;

            // troca para o estado de morte
            playerStateManager.SwitchState(playerStateManager.dieState);

            
        }
    }



        
    




    // private void HandleMovement()
    // {

    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         isDead = true;
    //        // PlayDeadAnimation();
    //     }

    //     if (speed > 0.1f && !isDead)
    //     {
    //        // PlayRunAnimation();
    //     }
    //     else if (!isDead)
    //     {
    //        // PlayIdleAnimation();
    //     }
    // }






    // public void PlayRunAnimation()
    // {
    //     animatorManager.Play(AnimatorManager.AnimationType.RUN);
    // }



    // public void PlayDeadAnimation()
    // {
    //     animatorManager.Play(AnimatorManager.AnimationType.DEAD);
    // }
}
