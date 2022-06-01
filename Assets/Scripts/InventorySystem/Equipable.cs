using FPSController;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

public class Equipable : Interactable
{
    [Space, Header("Equipable Settings")] 
    [SerializeField] private bool isEquipable = true;
    [ShowIf("isEquipable")] [SerializeField] public Item itemToEquip;
    [ShowIf("isEquipable")] [SerializeField] private int amount;
    [ShowIf("isEquipable")] [SerializeField] private bool destroyOnEquip = true;
    

    public bool IsEquipable => isEquipable;
    public int Amount => amount;
    public bool DestroyOnEquip => destroyOnEquip;

    public override void OnInteract(InventoryData inventoryData)
    {
        if (inventoryData.AddItem(itemToEquip))
        {
            if (destroyOnEquip)
            {
                Destroy(gameObject);
            }
            Debug.Log("Equipped: " + gameObject.name);
        }
        base.OnInteract(inventoryData);
    }

    public virtual void OnEquip(InventoryData inventoryData)
    {
        if (inventoryData.AddItem(itemToEquip))
        {
            if (destroyOnEquip)
            {
                Destroy(gameObject);
            }
            Debug.Log("Equipped: " + gameObject.name);
        }
    }
}