using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GGM.Singleton;
using Microsoft.Unity.VisualStudio.Editor;

namespace GGM.Item
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite UiIcon;
    }

    public class ItemsManager : Singleton<ItemsManager>
    {
        public List<ItemSetup> itemSetups;

        //public SOInt air;

        // Start is called before the first frame update
        void Start()
        {
            ReSet();
        }

        private void ReSet()
        {
            foreach(var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        // Update is called once per frame
        public void AddByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value += amount;
            if(item.soInt.value < 0) item.soInt.value = 0;  
        }

        [NaughtyAttributes.Button]
        private void addCoin()
        {
            AddByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void addLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }
        
    }
    
}

