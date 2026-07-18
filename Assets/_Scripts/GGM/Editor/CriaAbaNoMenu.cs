using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class CriaAbaNoMenu
{
    #if UNITY_EDITOR

        [UnityEditor.MenuItem("GGM/MEU_MENU")]
        public static void meu_menu() 
        {
            Debug.Log("testando aba no menu principal");
        }

        [UnityEditor.MenuItem("GGM/MEU_MENU_COM_ATALHO %g")]
        public static void meu_menu_com_atalho() 
        {
            Debug.Log("testando aba no menu principal");
        }


        [MenuItem("GGM/CRIAR_NOVO_OBJETO %q")]
        public static void criar_novo_objeto()
        {
            // Carrega o prefab da pasta Resources/Prefabs
            GameObject prefab = Resources.Load<GameObject>("Prefabs/RandomObject");

        if (prefab != null)
        {
            // Instancia o objeto na cena
            GameObject novo = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            if (novo != null)
            {
                // Define a posição e rotação desejada
                novo.transform.position = new Vector3(-3f, -1.5f, 3f);   // exemplo
                novo.transform.rotation = Quaternion.identity;        // rotação padrão

                // Seleciona o objeto recém-criado no Editor
                Selection.activeObject = novo;
            }
        }

            else
            {
                Debug.LogWarning("Prefab não encontrado em Resources/Prefabs/RandomObject");
            }
        }




    #endif
}
