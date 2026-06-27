using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Item
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform itemContainer;

        public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItems();
        }

        private void CreateItems ()
        {
            foreach(var setup in ItemsManager.Instance.itemSetups)
            {
                var item = Instantiate(prefabLayout, itemContainer);
                item.Load(setup);
                itemLayouts.Add(item);
            }    
        }
    }

}