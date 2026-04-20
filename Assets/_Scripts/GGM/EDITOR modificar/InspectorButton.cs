using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandonObject))]

public class InspectorButton : Editor
{
    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // inspectorButton.umAtributoQualquer = EditorGUILayout.IntField("Um Atributo Qualquer", inspectorButton.umAtributoQualquer);
        // EdotirGUILayout.LabelField("Um Atributo Qualquer", inspectorButton.umAtributoQualquer.ToString());
        // EditorGUILayout.IntField("Um Atributo Qualquer", inspectorButton.umAtributoQualquer);
        EditorGUILayout.HelpBox("Clique no botão abaixo para exibir inventário", MessageType.Info);

        GUI.color = Color.green;
        RandonObject myScript = (RandonObject)target;
        if (GUILayout.Button("EXIBIR INVENTÁRIO"))
        {
            myScript.showItems();
        }
    }
}
