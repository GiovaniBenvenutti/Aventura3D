using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor3D : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color _defaultColor;

    private Tween _currentTween;

    public string collorParameterName = "_EmissionColor";

    void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // void Start()
    // {
    //     _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    // }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        Debug.Log("Flash");
        if (meshRenderer != null && !_currentTween.IsActive())
            _currentTween = meshRenderer.material.DOColor(flashColor, collorParameterName, flashDuration).SetLoops(2, LoopType.Yoyo);

        if (skinnedMeshRenderer != null && !_currentTween.IsActive())
             _currentTween = skinnedMeshRenderer.material.DOColor(flashColor, collorParameterName, flashDuration).SetLoops(2, LoopType.Yoyo);
        
    }
}
