using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Run_State : PlayerBaseState
{  
    private Player spc;
    private Animator animator;

    void Awake()
    {
        spc = GetComponent<Player>();
        animator = spc.animator; // usa o animator do controler para garantir consistência
    }

    public override void OnStateEnter(PlayerStateManager player)
    {
        Debug.Log("Enter State: RUN");
        animator.SetTrigger("run");
        animator.speed = 1.3f; // acelera só a animação
    }

    public override void OnStateStay(PlayerStateManager player)
    {
        float inputAxisVertical = Input.GetAxis("Vertical");

        // movimento acelerado
        Vector3 move = spc.transform.forward * inputAxisVertical * spc.speedRun;

        spc._vSpeed -= spc.gravity * Time.deltaTime;
        move.y = spc._vSpeed;

        spc.characterController.Move(move * Time.deltaTime);

        if (inputAxisVertical == 0 || spc.isDead || !Input.GetKey(spc.runKey))
        {
            player.SwitchState(player.idleState);
        }
    }


    public override void OnStateExit(PlayerStateManager player)
    {
        Debug.Log("Exit State: RUN");
        animator.speed = 1f; 
    }

        // public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
        // {
        //     Debug.Log("Collision Enter in State: RUN");
        // }
}
