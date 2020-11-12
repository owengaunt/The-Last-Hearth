using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerINV : MonoBehaviour
{

    public InventoryObject inventory;
    public InventoryObject equipment;


    public Attribute[] attributes;

    public void OnTriggerEnter(Collider other)
    {
        var grounditem = other.GetComponent<GroundItem>();
        if (grounditem)
        {
            Item _item = new Item(grounditem.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventory.Load();
            equipment.Load();
        }
    }


    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }


    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

}


[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerINV parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerINV _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }


    public void AttributeModified()
    {
        parent.AttributeModified(this); 
    }

}