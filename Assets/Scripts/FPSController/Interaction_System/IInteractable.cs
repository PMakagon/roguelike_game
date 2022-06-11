using InventorySystem;

namespace FPSController.Interaction_System
{    
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool IsInteractable { get; }
        string TooltipMessage { get; }
        // void OnInteract();
        void OnInteract(InventoryData inventoryData);
    }
}  
