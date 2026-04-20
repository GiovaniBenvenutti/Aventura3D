using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimated : MonoBehaviour
{
    public List<GameObject> myObjects;

    public float delayBetweenObjects = 0.1f;
    public float animationDuration = 0.5f;

    [Header("Animation Settings")]
    public Animator animator;
    public string triggerShow = "Show";
    public string triggerHide = "Hide";
    private bool _isShowing = false;

    public void showItems()
    {
        if(_isShowing)
        {
            _isShowing = false;
            animator.SetTrigger(triggerHide);
        }
        else
        {
            _isShowing = true;
            animator.SetTrigger(triggerShow); 
        }
    }


}
