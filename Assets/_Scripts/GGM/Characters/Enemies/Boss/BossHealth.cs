using System;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<BossHealth> OnDamage;
    public Action<BossHealth> OnKill;

    void Awake()
    {
        Init();
    }
    
    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        Debug.Log("Boss morreu");
        if(destroyOnKill) Destroy(gameObject, 3f);
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5f);
    }

    public void Damage(float damage)
    {
        Debug.Log("Boss sofreu dano");
        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }

}
