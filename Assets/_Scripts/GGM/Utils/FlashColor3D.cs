using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor3D : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color _defaultColor;

    private Tween _currentTween;

    void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
    }


    void Start()
    {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }


    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
            _currentTween = meshRenderer.material.DOColor(flashColor, "_EmissionColor", flashDuration).SetLoops(2, LoopType.Yoyo);
    }
}
