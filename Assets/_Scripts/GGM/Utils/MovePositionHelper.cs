using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePositionHelper : MonoBehaviour
{
    public Transform startTransform;
    public Ease easeType = Ease.OutQuad;
    public float moveDuration = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startTransform.position;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void MovePosition()
    {
        if (transform.parent == null)
        {
            Debug.LogWarning("Este objeto deve possuir um parent!");
            return;
        }
        if (startTransform == null)
        {
            Debug.LogWarning("O startTransform não foi atribuído!");
            return;
        }

        // pega a posição do objeto pai
        Vector3 targetPos = transform.parent.position;

        // move até a posição do parent em 1 segundo
        transform.DOMove(targetPos, moveDuration).SetEase(easeType);
    }

}
