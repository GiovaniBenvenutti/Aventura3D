using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;


public class Player_Idle_State : PlayerBaseState
{

        public override void OnStateEnter(PlayerStateManager player)
        {
            Debug.Log("Enter State: IDLE");
        }

        public override void OnStateStay(PlayerStateManager player)
        {
            //Debug.Log("Stay in State: IDLE");
        }

        public override void OnStateExit(PlayerStateManager player)
        {
            Debug.Log("Exit State: IDLE");
        }

        // public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
        // {
        //     Debug.Log("Collision Enter in State: IDLE");
        // }

}
