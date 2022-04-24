
using FPSController;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class EquipableBase : Interactable
{
    [Space, Header("Equipable Settings")] 
    [SerializeField] private bool equipable = true;
    [ShowIf("equipable")] [SerializeField] public Item itemToEquip;
    [ShowIf("equipable")] [SerializeField] private int amount;
    [ShowIf("equipable")] [SerializeField] private bool destroyOnEquip = true;
    [SerializeField] private UnityEvent actionOnEquipped;
    
    public bool IsEquipable => equipable;
    public void OnEquip(InventoryData inventoryData)
    {
        if (inventoryData.AddItem(itemToEquip))
        {
            actionOnEquipped?.Invoke();
            Debug.Log("Equipped: " + gameObject.name);
            if (destroyOnEquip)
            {
                Destroy(gameObject);
            }
        }
    }
}