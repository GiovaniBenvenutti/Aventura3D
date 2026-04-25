using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using NaughtyAttributes;
using System;

namespace GGM.FSM
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;
        private StateBase _currentState;
        public float timeToStartGame = 1f;

        // public StateMachine(T state)
        // {
        //     dictionaryState = new Dictionary<T, StateBase>();
        //     SwitchState(state);
        // }

        public StateBase CurrentState
        {
            get { return _currentState; }
        }

        public void Init() 
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }
        
        public void SwitchState(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }


        // #if UNITY_EDITOR
        //     #region Debug

        //         [Button]
        //         private void DebugSwitchStateIDLE()
        //         {
        //             SwitchState(States.IDLE);
        //         }

        //     #endregion
        // #endif

    }
}
