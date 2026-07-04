using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";

        public float duration = 2f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(compareTag))
            {
                Collect();
            }

        /*    if(other.CompareTag("Player"))
            {
                ClothManager.Instance.GetClothByType(ClothType.ORIGINAL);
            }   */
        }

        public virtual void Collect()
        {
            //ClothManager.Instance.ChangeTexture(ClothType.SPEED);
            var setup = ClothManager.Instance.GetClothByType(clothType); 
            Player.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
