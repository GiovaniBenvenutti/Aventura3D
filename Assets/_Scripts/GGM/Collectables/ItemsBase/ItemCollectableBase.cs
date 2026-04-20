using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "CoinCollector";
    //public float timeToHide = 2f;
    //public GameObject graficItem;

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
        //Debug.Log("Item coletado pelo itemCollectableBase: " + gameObject.name);
        // if(graficItem != null)
        // {
        //     graficItem.SetActive(false);
        // }
        //Debug.Log("Collect de itemCollectableBase foi chamado");

        gameObject.SetActive(false);
        OnCollect();
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
            newAudio.Play(); 
            Destroy(newAudio.gameObject, newAudio.clip.length); // limpa depois que terminar de tocar
        }
    }
}
