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
        [Tooltip("O que for incluido no codigo aqui aparecerá como opção para items da interface, crie um novo prefab de item para armazenar imagem, SO e o que mais for preciso")]
        COIN,
        LIFE_PACK,
        AIR_DROP
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOIntString soIntString;
        public Sprite UiIcon;
    }

    public class ItemsManager : Singleton<ItemsManager>
    {
        [Tooltip("Basta adicionar um ite aqui para ele ser acrescentado na iterface")]
        public List<ItemSetup> itemSetups;

        //public SOInt air;

        // Start is called before the first frame update
        void Start()
        {
            ReSet();
            LoadItemsFromSave();
        }

        private void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int)SaveManager.Instance.setup.coins);
            AddByType(ItemType.LIFE_PACK, (int)SaveManager.Instance.setup.health);
            AddByType(ItemType.AIR_DROP, (int)SaveManager.Instance.setup.airDrop);
        }

        private void ReSet()
        {
            foreach(var i in itemSetups)
            {
                i.soIntString.intValue = 0;
            }
        }

        // Update is called once per frame
        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }

        // Update is called once per frame
        public void AddByType(ItemType itemType, int amount = 0)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soIntString.intValue += amount;
            if(item.soIntString.intValue < 0) item.soIntString.intValue = 0;  
        }


        #region Debug

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
        
        #endregion
    }
    
}

