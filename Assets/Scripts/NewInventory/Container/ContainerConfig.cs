using System.Collections.Generic;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;
using UnityEngine;

namespace LiftGame.NewInventory.Container
{
    [CreateAssetMenu(fileName = "ContainerConfig")]
    public class ContainerConfig : ScriptableObject
    {
        [SerializeField] private string containerName;
        [SerializeField] private int widht;
        [SerializeField] private int height;
        [SerializeField] private List<ItemDefinition> allowedItems;
        [SerializeField] private InventoryRenderMode renderMode = InventoryRenderMode.Grid;
        
        public string ContainerName => containerName;

        public int Widht => widht;

        public int Height => height;

        public List<ItemDefinition> AllowedItems => allowedItems;
        
        public InventoryRenderMode RenderMode => renderMode;
    }
}