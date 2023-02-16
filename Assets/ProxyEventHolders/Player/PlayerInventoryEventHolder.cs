using System;

namespace LiftGame.ProxyEventHolders.Player
{
    public static class PlayerInventoryEventHolder
    {
        public static event Action OnInventoryLoad; 
        public static event Action OnInventoryOpen;
        public static event Action OnInventoryClose;
        public static event Action OnInventoryChange;

        public static event Action OnContainerOpen;
        public static event Action OnItemAddedToBag;
        public static event Action OnItemAddedToCase;
        public static event Action OnItemAddedToEquipmentSlot;
        public static event Action OnItemAddedToPocket;
        public static event Action OnEquipmentAdd;
        
        public static void BroadcastOnInventoryLoad()
        {
            OnInventoryLoad?.Invoke();
        } 
        
        public static void BroadcastOnInventoryOpen()
        {
            OnInventoryOpen?.Invoke();
        }
        
        public static void BroadcastOnInventoryClose()
        {
            OnInventoryClose?.Invoke();
        }
        public static void BroadcastOnInventoryChange()
        {
            OnInventoryChange?.Invoke();
        }
        
        public static void BroadcastOnItemAddedToBag()
        {
            OnItemAddedToBag?.Invoke();
            OnInventoryChange?.Invoke();
        }
        
        public static void BroadcastOnItemAddedToCase()
        {
            OnItemAddedToCase?.Invoke();
            OnInventoryChange?.Invoke();
        }
        
        public static void BroadcastOnItemAddedToEquipmentSlot()
        {
            OnItemAddedToEquipmentSlot?.Invoke();
            OnInventoryChange?.Invoke();
        }
        
        public static void BroadcastOnItemAddedToPocket()
        {
            OnItemAddedToPocket?.Invoke();
            OnInventoryChange?.Invoke();
        }
        
        public static void BroadcastOnEquipmentAdd()//??
        {
            OnEquipmentAdd?.Invoke();
            OnInventoryChange?.Invoke();
        }
        
        public static void SendOnWorldContainerOpen()
        {
            OnContainerOpen?.Invoke();
        }
    }
}