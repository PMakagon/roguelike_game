using System.Collections.Generic;
using LiftGame.InventorySystem.Items;

namespace LiftGame.InventorySystem
{
    public interface IItemContainer
    {
        public int Capacity();
        public bool HasNothing();
        public bool IsFull();
        public void ClearContainer();
        public bool AddItem(IItem item, int amount);
        public void RemoveItem(IItem item);
        public string ContainerName { get; }
        public List<IItem> Items { get; }
    }
}