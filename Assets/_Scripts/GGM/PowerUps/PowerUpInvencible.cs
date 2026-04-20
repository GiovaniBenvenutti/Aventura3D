using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{
    //[Header("Speed Up Settings")]
    //public float amountToSpeed = 5f; // quantidade a ser adicionada à velocidade do jogador

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetInvencible();
        PlayerController.Instance.SetPowerUpText("Invencible");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.SetInvencible(false);
        PlayerController.Instance.SetPowerUpText("");
    }
}
