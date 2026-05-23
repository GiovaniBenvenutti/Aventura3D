using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase3D gunBase;
    public Transform gunPosition;
    private GunBase3D _currentGun;

    protected override void Init()
    {
        base.Init();
        createGun();
        inputs.GamePlay.shoot.performed += ctx => StartShoot();
        inputs.GamePlay.shoot.canceled += ctx => CancelShoot();
    }

    private void createGun()
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject);

        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        
    }
    
    private void StartShoot()
    {
        _currentGun.StartShoot();
        //if (playerController.IsDead) return;

        // Implement shooting logic here
        Debug.Log("Player start shoots!");
    }

    private void CancelShoot()
    {
        //if (playerController.IsDead) return;

        // Implement shooting logic here
        Debug.Log("Player cancels shoot!");
        _currentGun.StopShoot();
    }
}
