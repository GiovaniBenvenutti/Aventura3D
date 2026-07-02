using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HealthBase : MonoBehaviour, IDamageable
{
    public Action<HealthBase> OnKill;
    public Action<HealthBase> OnDamage;
    public List<UiHealthUpdater> uiUpdaters;
    public bool isPlayer = false;
    public float startLife = 20f;
    public bool destroiOnKill = false;
    public float delayToKill = 0f;

    public float _currentLife;
    private bool _isDead = false;
    private FlashColor3D _flashColor;

    // void OnValidade()
    // {
    // }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        _flashColor = GetComponent<FlashColor3D>();
        uiUpdaters = FindObjectsOfType<UiHealthUpdater>().ToList();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
        UpDateUI();
    }

    public void ResetLife()
    {
        Init();
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(1f);
    }


    public void Damage(float damage)
    {
        if(_isDead) return;

        _currentLife -= damage;
        UpDateUI();
        OnDamage?.Invoke(this);

        Debug.Log("Sofreu dano");

        if(_currentLife <= 0)
        {
            Kill();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if(destroiOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
        OnKill?.Invoke(this);
    }

    public void Damage(float damage, Vector3 direction)
    {
        Damage(damage);
    }

    private void UpDateUI()
    {
        if(uiUpdaters != null && isPlayer)
        {
            foreach(var updater in uiUpdaters)
            {
                updater.uiUpdateValue(_currentLife / startLife);
            }
        }
    }
}
