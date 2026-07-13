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
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {

        CheckPointBase checkPoint = checkPoints.Find(cp => cp.key == lastCheckPointKey);
        return checkPoint != null ? checkPoint.transform.position : Vector3.zero;

    }
}
