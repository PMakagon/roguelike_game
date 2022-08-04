using PlayerPowerSystem;

namespace PlayerEquipment
{
    public interface IPowerEquipment : IPlayerEquipment
    {
        PowerData PowerData { get; set; }
        bool IsTurnedOn { get; set; } 
        void TurnOn();
        void TurnOff();
    }
}