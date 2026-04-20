using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BounceHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = 0.05f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E)) Bounce();
    }

    public void Bounce(float bounce = 0f)
    {
        if(bounce == 0f) bounce = scaleBounce;
        // Executa o bounce e chama a corrotina ao terminar
        transform.DOScale(bounce, scaleDuration)
                 .SetEase(ease)
                 .SetLoops(2, LoopType.Yoyo)
                 .OnComplete(() => StartCoroutine(ResetScaleCoroutine()));
    }

    private IEnumerator ResetScaleCoroutine()
    {
        // Espera um frame para garantir que o tween terminou
        yield return null;
        transform.localScale = Vector3.one;
    }
}
