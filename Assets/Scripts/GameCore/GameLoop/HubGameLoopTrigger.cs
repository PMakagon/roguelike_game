using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace LiftGame.GameCore.GameLoop
{
    public class HubGameLoopTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent actionOnTrigger;
        private ILevelGameLoopEventHandler _levelGameLoopEventHandler;


        [Inject]
        private void Construct(ILevelGameLoopEventHandler levelGameLoopEventHandler)
        {
            _levelGameLoopEventHandler = levelGameLoopEventHandler;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_levelGameLoopEventHandler.IsPlayerOnLevel)
                {
                    actionOnTrigger?.Invoke();
                    _levelGameLoopEventHandler.EndLoop();
                }
            }
        }
    }
}