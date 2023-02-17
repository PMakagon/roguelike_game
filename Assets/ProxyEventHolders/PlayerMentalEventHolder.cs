using System;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;

namespace LiftGame.ProxyEventHolders
{
    public static class PlayerMentalEventHolder
    {
        public static event Action<PlayerMentalData, int> OnStressApplied;
        public static event Action<int> OnStressTaken;
        public static event Action<StressState,StressState> OnStressStateChanged;

        public static void SendOnStressTaken(int stress)
        {
            OnStressTaken?.Invoke(stress);
        }
        
        public static void BroadcastOnStressApplied(PlayerMentalData playerMentalData,int damage)
        {
            OnStressApplied?.Invoke(playerMentalData,damage);
        }
        
        public static void BroadcastOnStressStateChanged(StressState prevState,StressState newState)
        {
            OnStressStateChanged?.Invoke(prevState,newState);
        }
        
        

    }
}