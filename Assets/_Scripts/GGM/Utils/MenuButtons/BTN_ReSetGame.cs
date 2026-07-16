using System.Collections;
using System.Collections.Generic;
using GGM.Item;
using UnityEngine;

public class BTN_ReSetGame : MonoBehaviour
{
    public LoadSceneHelper loadSceneHelper;

    void Awake()
    {
        loadSceneHelper = FindObjectOfType<LoadSceneHelper>();
    }

    public void ReSetGame()
    {
        SaveManager.Instance.CreateNewSave();
        ItemsManager.Instance.ReSet();
        CheckPointManager.Instance.ResetCheckPoint();
        loadSceneHelper.Load(1); // carrega a cena 1
        
        //Time.timeScale = 1f;    // REINICIA O TIME SCALE PARA 1 AO CARREGAR CENA
    }
}
