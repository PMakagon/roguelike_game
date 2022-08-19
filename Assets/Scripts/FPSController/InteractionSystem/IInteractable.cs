﻿using LiftGame.InventorySystem;

namespace LiftGame.FPSController.InteractionSystem
{    
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool IsInteractable { get; }
        string TooltipMessage { get; set; }
        void OnInteract(InventoryData inventoryData);
    }
}  