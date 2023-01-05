using System.Collections.Generic;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using UnityEngine;

namespace LiftGame.Inventory.Container
{
    [CreateAssetMenu(fileName = "ContainerConfig")]
    public class ContainerConfig : ScriptableObject
    {
        [SerializeField] private string containerName;
        [SerializeField] [Range(1,5)]private int widht;
        [SerializeField] [Range(1,4)] private int height;
        [SerializeField] private List<ItemDefinition> allowedItems;
        [SerializeField] private InventoryRenderMode renderMode = InventoryRenderMode.Grid;
        
        public string ContainerName => containerName;

        public int Widht => widht;

        public int Height => height;

        public List<ItemDefinition> AllowedItems => allowedItems;
        
        public InventoryRenderMode RenderMode => renderMode;
    }
}