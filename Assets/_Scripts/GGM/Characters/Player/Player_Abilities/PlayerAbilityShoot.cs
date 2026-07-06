using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase3D> gunBase;
    public Transform gunPosition;
    private GunBase3D _currentGun;
    public FlashColor3D flashColor;

    protected override void Init()
    {
        base.Init();
        createGun();
        inputs.GamePlay.shoot.performed += ctx => StartShoot();
        inputs.GamePlay.shoot.canceled += ctx => CancelShoot();
        inputs.GamePlay.changeGun1.performed += ctx => ChangeGun(0);
        inputs.GamePlay.changeGun2.performed += ctx => ChangeGun(1);
        inputs.GamePlay.changeGun3.performed += ctx => ChangeGun(2);

    }


    private void createGun()
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject);

        _currentGun = Instantiate(gunBase[0], gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        
    }
    
    private void StartShoot()
    {
        _currentGun.StartShoot();
        //if (playerController.IsDead) return;
        flashColor?.Flash();

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

    public void ChangeGun(int index)
    {
        if (index < 0 || index >= gunBase.Count)
        {
            Debug.LogWarning("Invalid gun index!");
            return;
        }

        if (_currentGun != null && _currentGun != gunBase[index])
        {
            _currentGun.StopShoot();
            _currentGun = Instantiate(gunBase[index], gunPosition);
            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }
}
