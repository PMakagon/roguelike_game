using System;
using LiftGame.PlayerCore.HealthSystem;

namespace LiftGame.ProxyEventHolders.Player
{
    public static class PlayerHealthEventHolder
    {
        public static event Action<PlayerHealthData, float> OnDamageApplied;
        public static event Action<float> OnDamageTaken;
        public static event Action<PlayerHealthData, float> OnHealthRestored;
        public static event Action<HealthStatus,HealthStatus> OnHealthStatusChanged;
        public static event Action OnPlayerDied;

        public static void SendOnDamageTaken(float damage)
        {
            OnDamageTaken?.Invoke(damage);
        }
        
        public static void BroadcastOnDamageApplied(PlayerHealthData playerHealthData,float damage)
        {
            OnDamageApplied?.Invoke(playerHealthData,damage);
        }
        
        public static void BroadcastOnHealthStatusChanged(HealthStatus prevState,HealthStatus newState)
        {
            OnHealthStatusChanged?.Invoke(prevState,newState);
        }

        public static void BroadcastOnPlayerDied()
        {
            OnPlayerDied?.Invoke();
        }
        
    }
}