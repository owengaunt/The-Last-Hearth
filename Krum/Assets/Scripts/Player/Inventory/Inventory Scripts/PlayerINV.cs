using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerINV : MonoBehaviour
{

    public InventoryObject inventory; 

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventory.Load();
        }
    }



    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }

}
