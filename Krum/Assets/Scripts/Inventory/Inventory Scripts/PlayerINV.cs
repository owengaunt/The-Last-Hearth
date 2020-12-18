using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerINV : MonoBehaviour
{
    CharacterInput characterInput;
    public InventoryObject inventory;
    public InventoryObject equipment;

    public Camera pickupCam;
    public CanvasGroup PickUpMenu;
    public bool isLookingatObj;
    public bool isReadytoPickUp;
    public bool isPickedUp;
    public float range;

    public Attribute[] attributes;

    private Transform _food;
    private Transform _tools;
    private Transform _hat;
    private Transform _snowpants;
    private Transform _jacket;

    public Transform toolTransform;
    public Transform foodTransform;

    private BoneCombiner boneCombiner;
    private void Start()
    {
        characterInput = GetComponent<CharacterInput>();

        PickUpMenu.alpha = 0f;
        PickUpMenu.blocksRaycasts = false;

        isLookingatObj = false;
        isReadytoPickUp = false;
        isPickedUp = false;

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
                        case ItemType.Food:
                            Destroy(_food.gameObject);
                            break;
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
                        case ItemType.Food:
                            _food = Instantiate(_slot.ItemObject.characterDisplay, foodTransform).transform;
                            break;
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
                if (hit.collider.tag == "Food" || hit.collider.tag == "Drink" || hit.collider.tag == "Item")  
                {

                    if (characterInput.isMoving == false)
                    {
                        ShowPickUpUI();
                        isPickedUp = false;
                    }
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

            isLookingatObj = false;
        }
    }

    public void ShowPickUpUI()
    {
        isLookingatObj = true;

        if ((isLookingatObj == true) && (characterInput.isMoving == false))
        {
            PickUpMenu.alpha = 1f;
            PickUpMenu.blocksRaycasts = true;
        }
    
    }

    public void HidePickUpUI()
    {
        isLookingatObj = false;

        if (isLookingatObj == false)
        {
            PickUpMenu.alpha = 0f;
            PickUpMenu.blocksRaycasts = false;
        }
        
    }


    public void OnTriggerEnter()
    {
        isLookingatObj = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (isPickedUp == false)
        {
            if (Input.GetKey(KeyCode.F))
            {
                var grounditem = other.GetComponent<GroundItem>();
                if (grounditem)
                {
                    if (characterInput.isMoving == false)
                    {
                        Item _item = new Item(grounditem.item);
                        if (inventory.AddItem(_item, 1))
                        {
                            Destroy(other.gameObject);
                            isPickedUp = true;
                            PickUpMenu.alpha = 0f;
                            PickUpMenu.blocksRaycasts = false;
                        }
                    }
                }
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {
        isLookingatObj = false;
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