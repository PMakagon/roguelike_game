namespace LiftGame.PlayerEquipment
{
    public interface IPowerEquipment
    {
        bool IsTurnedOn { get; set; }
        void OnTurnOn();
        void OnTurnOff();
    }
}