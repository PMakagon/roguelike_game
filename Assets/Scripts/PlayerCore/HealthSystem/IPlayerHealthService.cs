using LiftGame.GameCore.Pause;

namespace LiftGame.PlayerCore.HealthSystem
{
    public interface IPlayerHealthService : IPauseable
    {
        void AddDamage(int damage);
        void SetHealthStartState();
        void SetHealthSafeState();
        void SetMortal(bool isMortal);
        void EnableDamage(bool isDamageAllowed);
        void EnableStressDamage(bool isStressable);
        bool IsPlayerDead();
    }
    
}