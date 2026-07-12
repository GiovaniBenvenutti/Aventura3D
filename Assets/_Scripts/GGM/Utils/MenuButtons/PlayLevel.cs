using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayLevel : MonoBehaviour
{
    public LoadSceneHelper loadSceneHelper;
    public int currentLevel;

    public TextMeshProUGUI uiTextName;

    void Awake()
    {
        loadSceneHelper = FindObjectOfType<LoadSceneHelper>();
    }

    void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;  
        //currentLevel = loadSceneHelper.currentLevel;  
    }

    void Update()
    {
        if(loadSceneHelper.currentLevel <  SceneManager.sceneCountInBuildSettings - 1)
        {
            uiTextName.text = "Play Next: \n  level " + (loadSceneHelper.currentLevel + 1);
        }
        else
        {
            uiTextName.text = "Play level 1";
        }
    }

    public void PlayNextLevel()
    {
        loadSceneHelper.LoadNext();
    }

    public void OnLoad(SaveSetup saveSetup)
    {
        uiTextName.text = "Play Next: \n level " + (loadSceneHelper.currentLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
