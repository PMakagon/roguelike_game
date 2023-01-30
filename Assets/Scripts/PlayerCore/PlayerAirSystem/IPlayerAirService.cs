using LiftGame.GameCore.Pause;

namespace LiftGame.PlayerCore.PlayerAirSystem
{
    public interface IPlayerAirService : IPauseable
    {
        void EnableAirSupply();
        void DisableAirSupply();
        void AddAir(float airToAdd);
        float GetCurrentLevel();
        float GetCurrentUsage();
        bool IsEmpty();
        
        void ResetAirData();
    }
}