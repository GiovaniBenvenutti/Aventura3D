using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoins : PowerUpBase
{
    [Header("Coin Collector Settings")]
    public float sizeAmount = 7f; // alcance de atração das moedas

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(sizeAmount);
        //PlayerController.Instance.SetPowerUpText("Invencible");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(1f); // resetar para o tamanho original
        //PlayerController.Instance.SetPowerUpText("");
    }
}
