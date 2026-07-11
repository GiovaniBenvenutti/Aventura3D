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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(Load), 0.1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.playerName = "Player1";
        _saveSetup.lastLevel = 0;
        _saveSetup.currentCloth = 0;
        _saveSetup.lastCheckPointIndex = 0;
        _saveSetup.newCheckPointIndex = 0;
        _saveSetup.health = 0;
        _saveSetup.coins = 0;
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

    public void SavePlayerName(string playerName)
    {
        _saveSetup.playerName = playerName;
        Save();
    }

    public void SaveLastLevel(int lastLevel)
    {
        _saveSetup.lastLevel = lastLevel;
        SaveItems();
        Save();
    }

    public void SaveItems()
    {
        _saveSetup.coins = ItemsManager.Instance.GetItemByType(ItemType.COIN).soIntString.intValue;
        _saveSetup.health = ItemsManager.Instance.GetItemByType(ItemType.LIFE_PACK).soIntString.intValue;
        Save();
    }

    public void SaveItems(int coins, int health)
    {
        _saveSetup.coins = coins;
        _saveSetup.health = health;
        Save();
    }

    private void SaveFile(string json)
    {
        /*
        // string path = Application.dataPath + "/save.txt";
        // string path = Application.persistentDataPath + "/save.txt";
        // string path = Application.streamingAssetsPath + "/save.txt";
        */
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

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
        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded?.Invoke(_saveSetup);
    }

    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public string playerName;
    public int lastLevel;
    public int currentCloth;
    public int lastCheckPointIndex;
    public int newCheckPointIndex;
    public float health;
    public float coins;
}
