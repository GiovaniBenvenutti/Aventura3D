using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Item;

public class PowerUpBase : ItemCollectableBase
{
    [Header("PowerUp Settings")]
    public float powerDuration = 5f; // duração do power-up em segundos

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
        PlayerControllerCasualGame.Instance.Bounce(1.2f);
    }

    protected virtual void StartPowerUp()
    {
        //Debug.Log("Power-up ativado: " + gameObject.name);
        // Aqui você pode adicionar o código para aplicar o efeito do power-up ao jogador
        // Por exemplo, aumentar a velocidade, invencibilidade, etc.

        // Inicia a contagem para desativar o power-up após a duração
        Invoke(nameof(EndPowerUp), powerDuration);
    }

    protected virtual void EndPowerUp()
    {
        //Debug.Log("Power-up desativado: " + gameObject.name);
        // Aqui você pode adicionar o código para remover o efeito do power-up ao jogador
        // Por exemplo, restaurar a velocidade normal, tornar o jogador vulnerável, etc.
    }

}
