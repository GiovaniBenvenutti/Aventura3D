using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.FSM
{
    public class StateBase
    {
        public virtual void OnStateEnter(Object o = null)
        {
            //Debug.Log("Enter State: " + this.name);
        } 

        public virtual void OnStateStay()
        {
            //Debug.Log("Stay in State: " + this.name);
        }

        public virtual void OnStateExit()
        {
            //Debug.Log("Exit State: " + this.name);
        }
    }
}
