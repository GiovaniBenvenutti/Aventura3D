using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GGM.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkPoints;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        //lastCheckPointKey = SaveManager.Instance.GetLastCheckPoint();
    }

    void Start()
    {
        //checkPoints = new List<CheckPointBase>(FindObjectsOfType<CheckPointBase>());
        lastCheckPointKey = SaveManager.Instance.GetLastCheckPoint();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        checkPoints = new List<CheckPointBase>(FindObjectsOfType<CheckPointBase>());
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public bool hasCheckPoint()
    {
        return lastCheckPointKey > 0;
    }

    public void saveCheckPoint(int key)
    {
        if (key > lastCheckPointKey) lastCheckPointKey = key;
        SaveManager.Instance.SaveNewCheckPointIndex(lastCheckPointKey);
    }

    public void ResetCheckPoint()
    {
        SaveManager.Instance.SaveNewCheckPointIndex(1);        
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        lastCheckPointKey = SaveManager.Instance.currentCheckPoint;
        CheckPointBase checkPoint = checkPoints.Find(cp => cp.key == lastCheckPointKey);
        Debug.Log("checkpointmanager diz lastcheckpoint é " + lastCheckPointKey);
        return checkPoint.transform.position;
       // return checkPoint != null ? checkPoint.transform.position : Vector3.zero;
    }
}
