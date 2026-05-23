using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase3D : MonoBehaviour
{
    public ProjectileBase3D prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = 0.2f;
    public float speed = 50f;

    //public KeyCode keyToShoot = KeyCode.Z;

    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
    
    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }
}
