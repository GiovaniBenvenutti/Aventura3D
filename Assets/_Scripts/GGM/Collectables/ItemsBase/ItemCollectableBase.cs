using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Item
{
    
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;
        public string compareTag = "Player";
        public float timeToHide = 2f;
        public GameObject graficItem;

        public new Collider collider;

        [Header("FX")]
        public new ParticleSystem particleSystem;
        public AudioSource audioSource;

        private void OnValidate() 
        {
            collider = GetComponent<Collider>();    
        }

        void OnTriggerEnter(Collider collision)     // para haver colisão o outro objeto deve ter rigidbody.
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect() // o que acontece quando o item é coletado
        {
            if(collider != null) collider.enabled = false;
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
        
            ItemsManager.Instance.AddByType(itemType);
        
        }
    }

}