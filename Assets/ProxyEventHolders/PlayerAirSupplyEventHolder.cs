using System;
using LiftGame.PlayerCore.PlayerAirSystem;

namespace LiftGame.ProxyEventHolders
{
    public static class PlayerAirSupplyEventHolder
    {
        public static event Action<PlayerAirData, float> OnAirRestoreApplied;
        public static event Action<float> OnAirRestored;
        public static event Action<PlayerAirData> OnAirLevelChanged;
        public static event Action<PlayerAirData> OnAirUsageChanged;
        public static event Action OnAirEmpty;
        public static event Action OnAirLow;
        
        public static void SendOnAirRestored(float airRestored)
        {
            OnAirRestored?.Invoke(airRestored);
        }

        public static void BroadcastOnAirRestoreApplied(PlayerAirData playerAirDataData,float airRestored)
        {
            OnAirRestoreApplied?.Invoke(playerAirDataData,airRestored);
        }
        
        public static void BroadcastOnAirLevelChanged(PlayerAirData playerAirDataData)
        {
            OnAirLevelChanged?.Invoke(playerAirDataData);
        }
        
        public static void BroadcastOnAirUsageChanged(PlayerAirData playerAirDataData)
        {
            OnAirUsageChanged?.Invoke(playerAirDataData);
        }

        public static void BroadcastOnAirEmpty()
        {
            OnAirEmpty?.Invoke();
        }public static void BroadcastOnAirLow()
        {
            OnAirLow?.Invoke();
        }
    }
}