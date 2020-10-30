using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Equipment", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public float atkBonus;
    public float dfsBonus;

    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
