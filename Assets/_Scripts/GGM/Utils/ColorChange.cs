using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public float duration = 1f;
    public MeshRenderer meshRenderer;

    public Color startColor = Color.white; 

    private Color _targetColor;

    private void OnValidate()
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    public void LerpColor ()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);  
        meshRenderer.materials[0].DOColor(_targetColor, duration).SetEase(Ease.Linear).SetDelay(.5f);  
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P)) LerpColor();
    }
}
