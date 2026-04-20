using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpHeight : PowerUpBase
{
    [Header("Power Up Height Settings")]
    public float amountToHeight = 2f; // quantidade a ser adicionada à altura do jogador
    public float animationDuration = 0.1f; // duração da animação de aumento de altura
    //public float duration = 5f; //ACHO QUE ESSE CARA AQUI É INUTIL....
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack; // tipo de easing para a animação

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountToHeight, powerDuration, animationDuration, ease);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight(animationDuration, ease);
    }
}
