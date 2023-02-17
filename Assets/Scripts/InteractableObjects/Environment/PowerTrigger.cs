using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;
using UnityEngine.Events;

namespace LiftGame.InteractableObjects.Environment
{
    public class PowerTrigger : MonoBehaviour
    {
        [SerializeField] private int chanceToInvoke = 100;
        [SerializeField] private int powerDamage = 40;
        [SerializeField] private UnityEvent actionOnTrigger;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var random = Random.Range(1, 100);
            if (chanceToInvoke >= random)
            {
                actionOnTrigger?.Invoke();
                PlayerPowerEventHolder.SendOnPowerDamageTaken(powerDamage);
            }
        }
    }
}