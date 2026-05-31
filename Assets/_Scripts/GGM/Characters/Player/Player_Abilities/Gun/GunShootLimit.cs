using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase3D
{
    public List<UiGunUpdater> uiGunUpdaters;
    public int maxShoot = 5;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _isRecharging;

    private void Awake()
    {
        GetAllUis();
        UpdateUi();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_isRecharging) yield break;
        while(true)
        {
            if(_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                checkRecharge();
                UpdateUi();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void checkRecharge()
    {
        if(_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _isRecharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float timer = 0;
        while(timer < timeToRecharge)
        {
            timer += Time.deltaTime;
            uiGunUpdaters.ForEach(ui => ui.uiUpdateValue(timer / timeToRecharge));

            yield return new WaitForEndOfFrame();   // usar um waitforseconds aqui não é recomendado, pois se o timeToRecharge for menor que o tempo de um frame, o timer nunca vai chegar no valor necessário para recarregar
        }   
        _currentShoots = 0;
        _isRecharging = false;
    }

    private void UpdateUi()
    {
        uiGunUpdaters.ForEach(ui => ui.uiUpdateValue(maxShoot, _currentShoots));
        
        // foreach(var ui in uiGunUpdaters)
        // {
        //     ui.uiUpdateValue(maxShoot, maxShoot - _currentShoots);
        // }
    }

    private void GetAllUis()
    {
        uiGunUpdaters = FindObjectsOfType<UiGunUpdater>().ToList();
    }

}
