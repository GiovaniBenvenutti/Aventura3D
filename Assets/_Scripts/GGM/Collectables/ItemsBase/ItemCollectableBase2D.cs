using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase2D : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect() // o que acontece quando o item é coletado
    {
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            ParticleSystem ps = Instantiate(particleSystem, transform.position, Quaternion.identity);
            ps.transform.SetParent(null);  // opcional: para removero o sistema de partículas do coletável antes que seja destruidom
            ps.Play();
        }

        if (audioSource != null) 
        { 
            AudioSource newAudio = Instantiate(audioSource, transform.position, Quaternion.identity); 
            newAudio.Play(); 
            Destroy(newAudio.gameObject, newAudio.clip.length); // limpa depois que terminar de tocar
        }
    }
}
