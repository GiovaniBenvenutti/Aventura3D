using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler e por ai vai...
{
    public float scaleMultiplier = 1.2f; // Multiplicador para aumentar o tamanho do botão
    public float animationDuration = 0.3f; // Duração da animação de escala
    public Ease animationEase = Ease.InOutBack; // Tipo de easing para a animação

    private Vector3 _defaultScale; // Valor de escala para aumentar o tamanho do botão
    private Tween _currentTween; // Referência para a animação atual
    
    // Start is called before the first frame update
    void Start()
    {
        _defaultScale = transform.localScale; // Armazena a escala original do botão
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // transform.localScale = new Vector3(scale, scale, scale) * scaleMultiplier; // Aumenta o tamanho do botão em 10%
        _currentTween = transform.DOScale(_defaultScale * scaleMultiplier, animationDuration).SetEase(Ease.OutBack); // Animação de escala suave
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // transform.localScale = new Vector3(scale, scale, scale); // Volta ao tamanho original
        _currentTween.Kill(); // Para a animação atual
        transform.DOScale(_defaultScale, animationDuration).SetEase(Ease.OutBack); // Animação de escala suave

    }
}
