using System;
using LiftGame.Inventory.Items;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.ProxyEventHolders
{
    public static class PlayerPowerEventHolder
    {
        public static event Action<int> OnPowerCellAdded;
        public static event Action<int> OnPowerCellRemoved;
        public static event Action<PlayerPowerData, float> OnPowerDamageApplied;
        public static event Action<float> OnPowerDamageTaken;
        public static event Action<PlayerPowerData, float> OnPowerRestoreApplied;
        public static event Action<float> OnPowerRestored;
        public static event Action<PlayerPowerData> OnPowerChanged;
        public static event Action<PlayerPowerData> OnMaxPowerChanged;
        public static event Action<PlayerPowerData> OnLoadChanged;
        public static event Action OnPowerOn;
        public static event Action OnPowerOff;
        
        public static void SendOnPowerCellAdded(int slotId)
        {
            OnPowerCellAdded?.Invoke(slotId);
        }
        public static void SendOnPowerCellRemoved(int slotId)
        {
            OnPowerCellRemoved?.Invoke(slotId);
        }
        
        public static void SendOnPowerDamageTaken(float damage)
        {
            OnPowerDamageTaken?.Invoke(damage);
        }

        public static void BroadcastOnPowerDamageApplied(PlayerPowerData playerPowerData,float damage)
        {
            OnPowerDamageApplied?.Invoke(playerPowerData,damage);
        }

        public static void SendOnPowerRestored(float powerRestored)
        {
            OnPowerRestored?.Invoke(powerRestored);
        }

        public static void BroadcastOnPowerRestoreApplied(PlayerPowerData playerPowerData,float powerRestored)
        {
            OnPowerRestoreApplied?.Invoke(playerPowerData,powerRestored);
        }

        public static void BroadcastOnPowerChanged(PlayerPowerData playerPowerData)
        {
            OnPowerChanged?.Invoke(playerPowerData);
        }
        public static void BroadcastOnMaxPowerChanged(PlayerPowerData playerPowerData)
        {
            OnMaxPowerChanged?.Invoke(playerPowerData);
        }
        
        public static void BroadcastOnLoadChanged(PlayerPowerData playerPowerData)
        {
            OnLoadChanged?.Invoke(playerPowerData);
        }

        public static void BroadcastOnPowerOn()
        {
            OnPowerOn?.Invoke();
            Debug.Log("POWER ON");
        }
        public static void BroadcastOnPowerOff()
        {
            OnPowerOff?.Invoke();
            Debug.Log("POWER OFF");
        }
    }
}