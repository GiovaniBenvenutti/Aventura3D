using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public float startLife = 20f;
    public bool destroiOnKill = false;
    public float delayToKill = 0f;

    [SerializeField] private float _currentLife;
    private bool _isDead = false;
    private FlashColor _flashColor;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        _flashColor = GetComponent<FlashColor>();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    // Update is called once per frame
    public void Damage(float damage)
    {
        if(_isDead) return;

        _currentLife -= damage;
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
        OnKill?.Invoke();
    }
}
