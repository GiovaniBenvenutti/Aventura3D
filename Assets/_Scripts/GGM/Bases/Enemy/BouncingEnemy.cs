using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouncingEnemy : MonoBehaviour
{
    public float bounceRange = 8f;
    public float bounceDuration = 3f;

    void Start()
    {
        // Move para +bounceRange no eixo X e volta, repetindo indefinidamente
        transform.DOMoveX(transform.position.x + bounceRange, bounceDuration)
                 .SetLoops(-1, LoopType.Yoyo)   // -1 = infinito, Yoyo = vai e volta
                 .SetEase(Ease.InOutSine);      // movimento suave
    }
}
