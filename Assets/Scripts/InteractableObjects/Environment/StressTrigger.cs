using LiftGame.PlayerCore.MentalSystem;
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
        private IPlayerMentalService _mentalService;
        public Collider _collider;


        [Inject]
        private void Construct(IPlayerMentalService mentalService)
        {
            _mentalService = mentalService;
        }
        
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
                    _mentalService.AddStress(stressDamage);
                    Debug.Log("AAA");
                }
                
            }
        }
    }
}