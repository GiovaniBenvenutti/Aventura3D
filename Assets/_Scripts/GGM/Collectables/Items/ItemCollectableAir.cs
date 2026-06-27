using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Item;

public class ItemCollectableAir : ItemCollectableBase
{
    protected override void OnCollect()
    {
        //ItemsManager.Instance.AddAir();
        base.OnCollect();
    }
}

