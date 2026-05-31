using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die_State : PlayerBaseState
{
    private Animator animator;
    private Player spc;

    void Awake()
    {
        spc = GetComponent<Player>();
        animator = spc.animator; // usa o animator do controler para garantir consistência
    }

    public override void OnStateEnter(PlayerStateManager player)
    {
        Debug.Log("Enter State: DIE");

        // zera velocidade vertical e horizontal
        spc._vSpeed = 0f;
        spc.speedVector = Vector3.zero;

        // dispara animação de morte
        animator.SetTrigger("dead");
        //Debug.Log("Trigger DEAD disparado");


        // marca como morto para bloquear inputs
        spc.isDead = true;
    }

    public override void OnStateStay(PlayerStateManager player)
    {
        Debug.Log("Stay in State: DIE");

        // impede qualquer movimento
        spc.characterController.Move(Vector3.zero);

    }

    public override void OnStateExit(PlayerStateManager player)
    {
        Debug.Log("Exit State: DIE");
        // normalmente não sai do estado de morte, mas se sair, pode resetar flags
        spc.isDead = false;
    }

        // public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
        // {
        //     Debug.Log("Collision Enter in State: DIE");
        // }
}
