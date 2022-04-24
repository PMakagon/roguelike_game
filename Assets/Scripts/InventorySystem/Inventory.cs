
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryData _inventoryData;
    [SerializeField] private InventoryCell inventoryCellTemplate;
    [SerializeField] private Transform inventoryContainer;

    public void OnEnable()
    {
        _inventoryData.ResetData();
        Render(_inventoryData.Items);
    }

    private void Update()
    {
        // CheckForNewItems();
    }

    public void ClearContainer()
    {
        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void Updateinventory(List<Item> items)
    {
        items.ForEach(item =>
        { 
            var cell = Instantiate(inventoryCellTemplate, inventoryContainer);
            cell.Render(item);
        });
    }
    
    public void Render(List<Item> items)
    {
        ClearContainer();

        Updateinventory(items);
    }

    public void CheckForNewItems()
    {
        if (_inventoryData.IsNeedUpdate)
        {
            Updateinventory(_inventoryData.Items);
        }
    }
}
