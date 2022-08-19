using LiftGame.PlayerCoreMechanics.PlayerPowerSystem;

namespace LiftGame.PlayerEquipment
{
    public interface IPowerEquipment : IPlayerEquipment
    {
        PlayerPowerData PlayerPowerData { get; set; }
        bool IsTurnedOn { get; set; } 
        void TurnOn();
        void TurnOff();
    }
}