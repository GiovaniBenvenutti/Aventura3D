using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump_State : PlayerBaseState
{
    public Player spc;

    void Awake()
    {
        spc = GetComponent<Player>();
    }


    public override void OnStateEnter(PlayerStateManager player)
    {
        Debug.Log("Enter State: JUMP");
        spc._vSpeed = spc.jumpForce; // impulso inicial
        spc.animator.SetTrigger("Jump");
    }

    public override void OnStateStay(PlayerStateManager player)
    {
        // quando voltar ao chão, troca para Idle
        if (spc.characterController.isGrounded && spc._vSpeed < 0)
        {
            spc.animator.SetTrigger("Land");
            player.SwitchState(player.idleState);
        }
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        Debug.Log("Exit State: JUMP");
    }

    // public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    // {
    //     Debug.Log("Collision Enter in State: JUMP");
    // }
}
