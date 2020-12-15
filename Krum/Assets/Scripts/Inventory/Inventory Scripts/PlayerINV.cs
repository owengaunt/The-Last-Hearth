using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerINV : MonoBehaviour
{
    public CharacterInput characterInput;
    public InventoryObject inventory;
    public InventoryObject equipment;

    public Camera pickupCam;
    public CanvasGroup PickUpMenu;
    public bool LookingatObj;
    public float range;

    public Attribute[] attributes;

    private Transform _default;
    private Transform _tools;
    private Transform _hat;
    private Transform _snowpants;
    private Transform _jacket;

    public Transform toolTransform;

    private BoneCombiner boneCombiner;
    private void Start()
    {
        characterInput = GetComponent<CharacterInput>();

        LookingatObj = false;

        boneCombiner = new BoneCombiner(gameObject);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }
    }

    public void OnRemoveItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
        {
            return;
        }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }


                if (_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Tools:
                            Destroy(_tools.gameObject);
                            break;
                        case ItemType.Hat:
                            Destroy(_hat.gameObject);
                            break;
                        case ItemType.SnowPants:
                            Destroy(_snowpants.gameObject);
                            break;
                        case ItemType.Jacket:
                            Destroy(_jacket.gameObject);
                            break;

                    }
                }


                break;
            case InterfaceType.Storage:
                break;
            default:
                break;
        }
    }


    public void OnAddItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
        {
            return;
        }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print($"Placed { _slot.ItemObject} on { _slot.parent.inventory.type }, Allowed Items: { string.Join(", ", _slot.AllowedItems)}");

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }

                if(_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Tools:
                            _tools = Instantiate(_slot.ItemObject.characterDisplay, toolTransform).transform; 
                            break;
                        case ItemType.Hat:
                            _hat = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay);
                            break;
                        case ItemType.SnowPants:
                            _snowpants = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay);
                            break;
                        case ItemType.Jacket:
                            _jacket = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay);
                            break;

                    }
                }


                break;
            case InterfaceType.Storage:
                break;
            default:
                break;
        }
    }

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

        RaycastHit hit;
        if (Physics.Raycast(pickupCam.transform.position, pickupCam.transform.forward, out hit, range))
        {
            if(hit.collider != null)
            {
                if ((hit.collider.tag == "Food" || hit.collider.tag == "Drink" || hit.collider.tag == "Item") && (characterInput.isMoving == false))
                {
                    ShowPickUpUI();
                }
                else
                {
                    HidePickUpUI();
                }
            }
        }

        if(characterInput.isMoving == true)
        {
            PickUpMenu.alpha = 0f;
            PickUpMenu.blocksRaycasts = false;

            LookingatObj = false;
        }
    }

    public void ShowPickUpUI()
    {
        LookingatObj = true;

        if ((LookingatObj == true) && (characterInput.isMoving == false))
        {
            PickUpMenu.alpha = 1f;
            PickUpMenu.blocksRaycasts = true;
        }
    
    }

    public void HidePickUpUI()
    {
        LookingatObj = false;

        if (LookingatObj == false)
        {
            PickUpMenu.alpha = 0f;
            PickUpMenu.blocksRaycasts = false;
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