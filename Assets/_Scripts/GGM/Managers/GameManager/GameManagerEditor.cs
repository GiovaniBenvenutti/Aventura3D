using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GGM.FSM;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(gameManager.stateMachine == null)
        {
            EditorGUILayout.HelpBox("State Machine is null", MessageType.Warning);
            return;
        }

        if(gameManager.stateMachine.CurrentState != null) EditorGUILayout.LabelField("Current State : " + gameManager.stateMachine.CurrentState.ToString());
        
        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if(showFoldout)
        {
            if(gameManager.stateMachine.dictionaryState != null)
            {                
                var keys = gameManager.stateMachine.dictionaryState.Keys.ToArray();
                var vals = gameManager.stateMachine.dictionaryState.Values.ToArray();

                for(int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} : {1}", keys[i].ToString(), vals[i].ToString()));
                }
            }
        }

        if (GUILayout.Button("Switch State IDLE"))
        {
            gameManager.stateMachine.SwitchState(GameManager.GameStates.INTRO);
        }
    }
}
