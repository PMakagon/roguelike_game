﻿using LiftGame.InventorySystem;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.PlayerCore
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
    public class PlayerData : ScriptableObject,IPlayerData
    {
        [SerializeField] private PlayerHealthData playerHealthData;
        [SerializeField] private PlayerMentalData playerMentalData;
        [SerializeField] private PlayerPowerData playerPowerData;
        [SerializeField] private InventoryData inventoryData;

        public void ResetData() ////пока не сделал save\load
        {
            playerHealthData.ResetData();
            playerMentalData.ResetData();
            playerPowerData.ResetData();
        }

        public InventoryData GetInventoryData()
        {
            return inventoryData;
        }
        public PlayerHealthData GetHealthData()
        {
            return playerHealthData;
        }
        public PlayerMentalData GetMentalData()
        {
            return playerMentalData;
        }

        public PlayerPowerData GetPowerData()
        {
            return playerPowerData;
        }
    }
}