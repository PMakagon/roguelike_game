using FPSController;

namespace InventorySystem.Equipable
{
    public interface IEquipable 
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        string TooltipMessage { get; }

        void Equip();
    }
}