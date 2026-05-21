using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase3D gunBase;
    protected override void Init()
    {
        base.Init();
        inputs.GamePlay.shoot.performed += ctx => StartShoot();
        inputs.GamePlay.shoot.canceled += ctx => CancelShoot();
    }
    
    private void StartShoot()
    {
        gunBase.StartShoot();
        //if (playerController.IsDead) return;

        // Implement shooting logic here
        Debug.Log("Player start shoots!");
    }

    private void CancelShoot()
    {
        //if (playerController.IsDead) return;

        // Implement shooting logic here
        Debug.Log("Player cancels shoot!");
        gunBase.StopShoot();
    }
}
