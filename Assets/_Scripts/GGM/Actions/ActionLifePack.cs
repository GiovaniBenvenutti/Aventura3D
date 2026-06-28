using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Item;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOIntString soIntString;

    // Start is called before the first frame update
    void Start()
    {
        soIntString = ItemsManager.Instance.GetItemByType(ItemType.LIFE_PACK).soIntString;        
    }

    private void RecoverLife()
    {
        if(soIntString.intValue > 0 && Player.Instance.health._currentLife < Player.Instance.health.startLife*0.95)
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
