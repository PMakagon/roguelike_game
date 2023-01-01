using System;
using LiftGame.PlayerCore.HealthSystem;

namespace LiftGame.ProxyEventHolders
{
    public static class PlayerHealthEventHolder
    {
        public static event Action<PlayerHealthData, int> OnDamageApplied;
        public static event Action<int> OnDamageTaken;
        public static event Action<PlayerHealthData, int> OnHealthRestored;
        public static event Action<HealthStatus,HealthStatus> OnHealthStatusChanged;
        public static event Action OnPlayerDied;

        public static void SendOnDamageTaken(int damage)
        {
            OnDamageTaken?.Invoke(damage);
        }
        
        public static void BroadcastOnDamageApplied(PlayerHealthData playerHealthData,int damage)
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