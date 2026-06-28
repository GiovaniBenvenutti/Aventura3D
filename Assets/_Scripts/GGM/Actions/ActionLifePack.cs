using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Item;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;

    // Start is called before the first frame update
    void Start()
    {
        soInt = ItemsManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;        
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemsManager.Instance.AddByType(ItemType.LIFE_PACK, -1);
            Player.Instance.health.ResetLife();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyCode)) RecoverLife();
    }
}
