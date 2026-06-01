using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase3D : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<ProjectileBase3D>())
        {
            Debug.Log($"Projectile collided with {collision.gameObject.name}");
            var damageable = collision.transform.GetComponent<IDamageable>();

            if (damageable != null) 
            {
                Vector3 direction = collision.transform.position - transform.position;
                direction = -direction.normalized *3f;
                // direction.y = 0f;
                direction.y *= 2f;

                damageable.Damage(damageAmount, direction);
            }
            
            Destroy(gameObject);
        }
    }

}
