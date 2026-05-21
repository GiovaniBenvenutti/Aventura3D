using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;

public abstract class PlayerBaseState : MonoBehaviour
{
        public abstract void OnStateEnter(PlayerStateManager player);

        public abstract void OnStateStay(PlayerStateManager player);

        public abstract void OnStateExit(PlayerStateManager player);

        //public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);


}
