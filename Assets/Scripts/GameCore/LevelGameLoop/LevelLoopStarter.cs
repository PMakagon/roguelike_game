using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace LiftGame.GameCore.LevelGameLoop
{
    public class LevelLoopStarter : MonoBehaviour
    {
        [SerializeField] private UnityEvent actionOnTrigger;
        private ILevelGameLoopEventHandler _levelGameLoopEventHandler;


        //MonoBehaviour injection
        [Inject]
        private void Construct(ILevelGameLoopEventHandler levelGameLoopEventHandler)
        {
            _levelGameLoopEventHandler = levelGameLoopEventHandler;
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartLoop();
        }

        public void StartLoop()
        {
            actionOnTrigger?.Invoke();
            _levelGameLoopEventHandler.TryStartLoop();
        }
    }
}