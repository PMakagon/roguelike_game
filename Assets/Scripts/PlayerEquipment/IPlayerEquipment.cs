namespace PlayerEquipment
{
    public interface IPlayerEquipment
    {
        bool IsTurnedOn { get; set; } 
        void Use();
        void Equip();
        void UnEquip();
        void TurnOn();
        void TurnOff();
    }
}