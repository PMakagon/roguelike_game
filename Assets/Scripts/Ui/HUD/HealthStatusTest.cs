using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace LiftGame.Ui.HUD
{
    public class HealthStatusTest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI healthStatus;
        private PlayerHealthData _playerHealthData;

        public PlayerHealthData PlayerHealthData
        {
            set => _playerHealthData = value;
        }


        public void UpdateStatus()
        {
            healthStatus.text = _playerHealthData.HealthStatus.ToString();
            health.text = _playerHealthData.Health.ToString();
        }
    }
}