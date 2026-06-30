using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class ChestBase : MonoBehaviour
{
    [Header("Chest")]
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public String triggerOpen = "open";
    private bool _opennedChest = false;

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = 0.2f;
    public Ease ease = Ease.OutBack;
    private float startScale;

    [Space(1)]
    public ChestItemBase chestItem;


    private void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_opennedChest) return;
        animator.SetTrigger(triggerOpen);
        _opennedChest = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();   
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            ShowNotification();            
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        if (_opennedChest) return;

        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }


}
