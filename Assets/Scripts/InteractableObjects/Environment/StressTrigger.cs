using LiftGame.PlayerCore.MentalSystem;
using LiftGame.ProxyEventHolders;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace LiftGame.InteractableObjects.Environment
{
    public class StressTrigger : MonoBehaviour
    {
        [SerializeField] private int chanceToInvoke = 80;
        [SerializeField] private int stressDamage = 40;
        [SerializeField] private UnityEvent actionOnTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var random = Random.Range(1, 100);
            if (chanceToInvoke>=random)
            { 
                actionOnTrigger?.Invoke();
                PlayerMentalEventHolder.SendOnStressTaken(stressDamage);
            }
        }
    }
}