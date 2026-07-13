using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using GGM.Singleton;
using GGM.Item;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    // public static Action<SaveSetup> FileLoaded;    // precisa da biblioteca using System;
    public Action<SaveSetup> FileLoaded;    // precisa da biblioteca using System;

    public int lastLevel;

    public SaveSetup setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(Load), 0.1f);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();

        _saveSetup.playerName = "Player1";
        _saveSetup.currentCloth = 0;

        _saveSetup.lastLevel = 0;

        _saveSetup.lastCheckPointIndex = 0;
        _saveSetup.newCheckPointIndex = 0;

        _saveSetup.health = 0;
        _saveSetup.coins = 0;
        _saveSetup.airDrop = 0;

        Save();
    }

    #region Save

    [NaughtyAttributes.Button("Save")]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }
    
    private void SaveFile(string json)
    {   // string path = Application.dataPath + "/save.txt"; string path = Application.persistentDataPath + "/save.txt"; string path = Application.streamingAssetsPath + "/save.txt";
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    public void SavePlayerName(string playerName)
    {
        _saveSetup.playerName = playerName;
        Save();
    }
    
    public void SavePlayerCloth(int currentCloth)
    {
        _saveSetup.currentCloth = currentCloth;
        Save();
    }

    public void SaveLastLevel(int lastLevel)
    {
        _saveSetup.lastLevel = lastLevel;
        this.lastLevel = lastLevel;
        SaveItems();
        
        Save();
    }
    public void SaveNewCheckPointIndex(int newCheckPointIndex)
    {
        _saveSetup.lastCheckPointIndex = _saveSetup.newCheckPointIndex;
        _saveSetup.newCheckPointIndex = newCheckPointIndex;
        Save();
    }

    public void SaveItems()
    {
        _saveSetup.coins = ItemsManager.Instance.GetItemByType(ItemType.COIN).soIntString.intValue;
        _saveSetup.health = ItemsManager.Instance.GetItemByType(ItemType.LIFE_PACK).soIntString.intValue;
        _saveSetup.airDrop = ItemsManager.Instance.GetItemByType(ItemType.AIR_DROP).soIntString.intValue;
        Save();
    }

    /*public void SaveItems(int coins, int health, int airDrop)
    {
        _saveSetup.coins = coins;
        _saveSetup.health = health;
        _saveSetup.airDrop = airDrop;
        Save();
    }*/


    #endregion

    #region Load

    [NaughtyAttributes.Button("Load")]
    private void Load()
    {
        string fileLoaded = "";
        if (File.Exists(_path)) 
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
            Debug.Log("Save file found." + _saveSetup);
        }
        else
        {
            CreateNewSave();
            Debug.Log("No save file found. Creating new save.");
            Save();
        }
        FileLoaded?.Invoke(_saveSetup);
    }

    public int GetLastLevel()
    {
        Load();
//        Debug.Log("savemanager diz que lastlevel é: " + lastLevel);
        //return _saveSetup.lastLevel;
        return lastLevel;
    }


    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public string playerName;
    public int currentCloth;

    public int lastLevel;

    public int lastCheckPointIndex;
    public int newCheckPointIndex;

    public float health;
    public float coins;
    public float airDrop;
}
