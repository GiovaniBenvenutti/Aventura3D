using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;
    void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;      
    }

    public void OnLoad(SaveSetup saveSetup)
    {
        uiTextName.text = "Play level " + (saveSetup.lastLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
