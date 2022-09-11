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


        [Inject]
        private void Construct(IPlayerData playerData)
        {
            _playerHealthData = playerData.GetHealthData();
        }
        
        private void Update()
        {
            healthStatus.text = _playerHealthData.HealthStatus.ToString();
            health.text = _playerHealthData.Health.ToString();
        }
    }
}