using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;

    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject weaponManager;

    [HideInInspector]
    public GameObject weapon;

    public bool playersWeapon;
    
    public void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager");

        if (!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++)
            {
                if(weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }


    public void Update()
    {
        if (equipped)
        {
            //preform action

        }
    }


    public void ItemUsage()
    {
        //weapon

        if(type == "Weapon")
        {
            weapon.SetActive(true);
            equipped = true;
        }

        //food
        if (type == "Food")
        {
            equipped = true;
        }

        //drink
        if (type == "Drink")
        {
            equipped = true;
        }

        //resource
        if (type == "Resource")
        {
            equipped = true;
        }
    }
}
