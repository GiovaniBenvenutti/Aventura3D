using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkPoints;

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
