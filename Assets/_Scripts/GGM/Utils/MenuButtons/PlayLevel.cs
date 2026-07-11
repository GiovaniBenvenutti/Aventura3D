using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        currentLevel = loadSceneHelper.currentLevel;  
    }

    public void PlayNextLevel()
    {
        loadSceneHelper.LoadNext();
    }

    public void OnLoad(SaveSetup saveSetup)
    {
        uiTextName.text = "Play Next \n  level " + (currentLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
