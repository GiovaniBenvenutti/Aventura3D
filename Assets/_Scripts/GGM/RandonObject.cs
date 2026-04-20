using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandonObject : MonoBehaviour
{
    public InventoryBase inventoryBase;

    public List<GameObject> objectsToRandomize;
    private GameObject myObject;


    void Awake() 
    {
        inventoryBase = GetComponent<InventoryBase>();

        foreach (var obj in objectsToRandomize)
        {
            obj.SetActive(false);
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        myObject = objectsToRandomize.GetRandom();
        myObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       // foreach (var obj in objectsToRandomize)
       // {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myObject.SetActive(false);
                var newObject = objectsToRandomize.GetNewRandom(myObject);
                myObject = newObject;
                myObject.SetActive(true);
                myObject.RandomizeColor();
            }
       // }
    }
    

    public void showItems()
    {
        inventoryBase.showItems();
    }

}
