using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
//  POSSO ACESSAR FILE/BUILD SETTINGS/SCENES IN BUILD PARA PEGAR O INDICE DAS MINHAS CENAS  
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
        Time.timeScale = 1f;    // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s);
        Time.timeScale = 1f;   // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA

    }
}
