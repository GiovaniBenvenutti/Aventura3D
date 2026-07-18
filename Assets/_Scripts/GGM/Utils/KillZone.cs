//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class KillZone : MonoBehaviour
{
    public BoxCollider collider;

    void Awake()
    {
        // garante que sempre terá referência ao BoxCollider
        collider = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // tenta obter o componente que implementa IDamageable
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(1000f);
            Debug.Log("morreu por tocar a kill box");
        }
    }
}
