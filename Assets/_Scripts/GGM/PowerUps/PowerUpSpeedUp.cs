using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedUp : PowerUpBase
{
    [Header("Speed Up Settings")]
    public float amountToSpeed = 5f; // quantidade a ser adicionada à velocidade do jogador

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerControllerCasualGame.Instance.PowerUpSpeedUp(amountToSpeed);
        PlayerControllerCasualGame.Instance.SetPowerUpText("Speed Up");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerControllerCasualGame.Instance.ResetSpeed();
        PlayerControllerCasualGame.Instance.SetPowerUpText("");
    }
}
