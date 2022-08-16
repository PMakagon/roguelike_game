using UnityEngine;

namespace InventorySystem.Items
{
    public interface IItem
    {
        string Name {  get; set; }
        ItemType ItemType { get; set; }
        Sprite UIIcon {  get; set; }
        string Description {  get; set; }
        bool Stackable { get; set; }
        void Use(InventoryData inventoryData);
    }
}