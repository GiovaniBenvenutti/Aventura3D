using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_exemple : MonoBehaviour
{
    public enum States
    {
        NONE,
        IDLE,
        RUNNING,
        JUMPING
    }

    public StateMachine<States> _stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new StateMachine<States>();
        _stateMachine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
