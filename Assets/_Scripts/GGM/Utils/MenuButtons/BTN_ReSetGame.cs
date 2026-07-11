using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_ReSetGame : MonoBehaviour
{
    public void ReSetGame()
    {
        SaveManager.Instance.CreateNewSave();
        //Time.timeScale = 1f;    // REINICIA O TIME SCALE PARA 1 AO CARREGAR CENA
    }
}
