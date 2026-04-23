using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FSM_exemple))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSM_exemple fsm = (FSM_exemple)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(fsm._stateMachine == null)
        {
            EditorGUILayout.HelpBox("State Machine is null", MessageType.Warning);
            return;
        }

        if(fsm._stateMachine.CurrentState != null) EditorGUILayout.LabelField("Current State : " + fsm._stateMachine.CurrentState.ToString());
        
        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if(showFoldout)
        {
             foreach (var state in fsm._stateMachine.dictionaryState)
             {
                 EditorGUILayout.LabelField(state.Key.ToString());
             }
        }

        if (GUILayout.Button("Switch State IDLE"))
        {
            fsm._stateMachine.SwitchState(FSM_exemple.States.IDLE);
        }
    }
}
