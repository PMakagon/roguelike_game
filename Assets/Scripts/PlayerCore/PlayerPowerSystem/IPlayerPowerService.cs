using LiftGame.GameCore.Pause;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    public interface IPlayerPowerService : IPauseable
    {
        void EnablePowerConsumption();
        void DisablePowerConsumption();
        float GetCurrentPower();
        float GetCurrentLoad();
        void AddLoad(float newLoad);
        void AddLoad(float newLoad, float penalty);
        void RemoveLoad(float load);
        void AddPower(float powerToAdd);
        void DamagePower(float powerDamage);
        bool IsNoPower();
        void ResetPowerData();
        public PlayerPowerData PlayerPowerData { get; }
        
    }
}