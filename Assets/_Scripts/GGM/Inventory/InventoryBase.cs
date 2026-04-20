using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryBase : MonoBehaviour
{
    public List<GameObject> myObjects;

    public float delayBetweenObjects = 0.1f;
    public float animationDuration = 0.5f;
    
    private bool _isShowing = false;

    void Awake()
    {
        hide();
    }

    public void hide()
    {
        _isShowing = false;
        foreach (GameObject g in myObjects)
        {
            g.SetActive(false);
        }
    }

    public void showItems()
    {
        if(_isShowing)
        {
            hide();
        }
        else
        {
            _isShowing = true;
            StartCoroutine(showObjects());
        }
    }

    IEnumerator showObjects()
    {
        foreach (GameObject g in myObjects)
        {
            yield return new WaitForSeconds(delayBetweenObjects);
            g.SetActive(true);
            g.transform.DOScale(0, animationDuration).From();
        }
    }
}
