using PlayerPowerSystem;

namespace PlayerEquipment
{
    public interface IPowerEquipment : IPlayerEquipment
    {
        PlayerPowerData PlayerPowerData { get; set; }
        bool IsTurnedOn { get; set; } 
        void TurnOn();
        void TurnOff();
    }
}