using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    public interface IItem
    {
        string Name {  get; set; }
        ItemType ItemType { get; set; }
        Sprite UIIcon {  get; set; }
        string Description {  get; set; }
        bool Stackable { get; set; }
        void Use(IPlayerData playerData);
    }
}