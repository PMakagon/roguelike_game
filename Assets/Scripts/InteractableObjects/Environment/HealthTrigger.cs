using LiftGame.PlayerCore.HealthSystem;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace LiftGame.InteractableObjects.Environment
{
    public class HealthTrigger : MonoBehaviour
    {
        [SerializeField] private int chanceToInvoke = 100;
        [SerializeField] private int healthDamage = 40;
        [SerializeField] private UnityEvent actionOnTrigger;
        private IPlayerHealthService _healthService;
        

        [Inject]
        private void Construct(IPlayerHealthService healthService)
        {
            _healthService = healthService;
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var random = Random.Range(1, 100);
                if (chanceToInvoke >= random)
                {
                    actionOnTrigger?.Invoke();
                    _healthService.AddDamage(healthDamage);
                    Debug.Log("Damade");
                }
            }
        }
    }
}