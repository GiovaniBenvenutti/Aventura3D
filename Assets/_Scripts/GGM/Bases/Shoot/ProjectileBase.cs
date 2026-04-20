using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;

    public float timeToDestroy = 1.5f;

    public float side = 1f;

    [Header("Sounds")]
    public AudioSource audioSourceShot;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);

        if (audioSourceShot != null) 
        { 
            AudioSource newAudio = Instantiate(audioSourceShot, transform.position, Quaternion.identity); 
            newAudio.Play(); 
            Destroy(newAudio.gameObject, newAudio.clip.length); // limpa depois que terminar de tocar
        }



    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var enemy = other.gameObject.GetComponent<HealthBase>();

        if (enemy != null)
        {
            enemy.Damage(1);
            Destroy(gameObject);
        }
    }
}
