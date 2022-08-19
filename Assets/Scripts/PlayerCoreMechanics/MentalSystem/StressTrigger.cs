using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace LiftGame.PlayerCoreMechanics.MentalSystem
{
    public class StressTrigger : MonoBehaviour
    {
        [SerializeField] private int chanceToInvoke = 80;
        [SerializeField] private UnityEvent actionOnTrigger;
        public Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var random = Random.Range(1, 100);
                if (chanceToInvoke>=random)
                { 
                    actionOnTrigger?.Invoke();
                    PlayerMentalController.onTriggerStress?.Invoke();
                    Debug.Log("AAA");
                }
                
            }
        }
    }
}