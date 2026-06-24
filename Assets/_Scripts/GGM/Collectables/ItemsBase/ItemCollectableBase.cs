using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide = 2f;
    public GameObject graficItem;

    [Header("FX")]
    public ParticleSystem particleSystem;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider collision)     // para haver colisão o outro objeto deve ter rigidbody.
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect() // o que acontece quando o item é coletado
    {
        if(graficItem != null) graficItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);        
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            ParticleSystem ps = Instantiate(particleSystem, transform.position, Quaternion.identity);
            ps.transform.SetParent(null);  // opcional: para removero o sistema de partículas do coletável antes que seja destruidom
            Debug.Log("Instanciou o sistema de partículas");
            ps.Play();
        }

        if (audioSource != null) 
        { 
            AudioSource newAudio = Instantiate(audioSource, transform.position, Quaternion.identity); 
            Debug.Log("Instanciou o AudioSource");
            newAudio.Play(); 
            Destroy(newAudio.gameObject, newAudio.clip.length); // limpa depois que terminar de tocar
        }
    }
}
