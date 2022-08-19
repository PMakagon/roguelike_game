using LiftGame.PlayerCoreMechanics.HealthSystem;
using TMPro;
using UnityEngine;

namespace LiftGame.Ui.HUD
{
    public class HealthStatusTest : MonoBehaviour
    {
        [SerializeField] private PlayerHealthController playerHealthController;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI healthStatus;

        private void Update()
        {
            healthStatus.text = playerHealthController.HealthStatus.ToString();
            health.text = playerHealthController.Health.ToString();
        }
    }
}