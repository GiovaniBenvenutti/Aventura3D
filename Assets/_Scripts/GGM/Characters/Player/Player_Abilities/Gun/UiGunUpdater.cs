using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiGunUpdater : MonoBehaviour
{
    public Image UiImage;

    [Header("Animation")]
    public float duration = 0.1f;
    public Ease ease = Ease.OutBack;

    private Tween _currentTween;


    private void OnValidate()
    {
        if (UiImage == null) UiImage = GetComponent<Image>();    
    }

    public void uiUpdateValue(float f)
    {
        UiImage.fillAmount = f;
    }

    public void uiUpdateValue(float max, float currente)
    {
        _currentTween?.Kill();
        if(_currentTween != null) _currentTween.Kill();
        _currentTween = UiImage.DOFillAmount(1 - (currente / max), duration).SetEase(ease);
    }
}
