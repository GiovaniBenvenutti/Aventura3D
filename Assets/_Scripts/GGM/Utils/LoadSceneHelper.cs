using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
//  POSSO ACESSAR FILE/BUILD SETTINGS/SCENES IN BUILD PARA PEGAR O INDICE DAS MINHAS CENAS
    public int currentLevel;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void Load(int i)
    {
        if(i < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(i);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        //SceneManager.LoadScene(i);
        Time.timeScale = 1f;    // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
    }

    public void LoadNext()
    {
        int nextLevel = SaveManager.Instance.GetLastLevel() + 1;
        Debug.Log("next level é: " + nextLevel);

        if(nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
            Debug.Log("LSH diz: scenecountinbuildsettings é igual " + SceneManager.sceneCountInBuildSettings);
            //return;
        }
        else
        {
            Debug.Log("LOADSCENEHELPER carregou menu");
            SceneManager.LoadScene(1);
            Debug.Log("LSH diz: scenecountinbuildsettings é igual " + SceneManager.sceneCountInBuildSettings);

        }
        Time.timeScale = 1f;    // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
    }

    public void Load(string s)
    {
        Debug.Log("chamou a troca de cena");

        SceneManager.LoadScene(s);
        Time.timeScale = 1f;   // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
    }

    // retorna a cena ativa
    public Scene GetActiveScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return SceneManager.GetActiveScene();
    }
}
