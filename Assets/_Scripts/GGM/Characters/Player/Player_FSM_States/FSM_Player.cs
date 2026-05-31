using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;


public class FSM_Player : MonoBehaviour
{
    public enum EnumStates
    {
        IDLE,
        RUNNING,
        JUMPING
    }

    public StateMachine<EnumStates> stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<EnumStates>(/*EnumStates.IDLE*/);
        stateMachine.Init();
        // stateMachine.RegisterStates(EnumStates.IDLE, new StateBase());
        // stateMachine.RegisterStates(EnumStates.RUNNING, new StateBase());
        // stateMachine.RegisterStates(EnumStates.JUMPING, new StateBase());
        stateMachine.SwitchState(EnumStates.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
