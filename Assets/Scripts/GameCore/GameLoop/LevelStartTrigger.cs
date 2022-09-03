using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace LiftGame.GameCore.GameLoop
{
    public class LevelStartTrigger : MonoBehaviour
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
            if (_levelGameLoopEventHandler.IsPlayerOnLevel) return;
            if (other.CompareTag("Player"))
            {
                actionOnTrigger?.Invoke();
                _levelGameLoopEventHandler.StartLoop();
            }
        }
    }
}