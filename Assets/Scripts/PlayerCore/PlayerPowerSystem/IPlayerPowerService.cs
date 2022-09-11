using System;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    public interface IPlayerPowerService
    {
        public event Action OnPowerChange;
        public event Action OnPowerOff;
        void AddPower(int powerToAdd);
        void SetActive(bool state);
        void ResetPowerData();
        bool IsNoPower();
        void ReducePowerLoad();
    }
}