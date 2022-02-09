using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftControllerBase : MonoBehaviour
    {
        private ILiftState _currentState;
        private ILiftState _previousState;
        private LiftStateFactory _states;
        private bool isDoorsOpen;


        private void Awake()
        {
            _states = new LiftStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
        }

        private void Start()
        {
        }

        private void Update()
        {
        
        }

        public void CloseDoors()
        {
            Debug.Log("Двери Закрываются");
            isDoorsOpen = false;
        }

        public void OpenDoors()
        {
            Debug.Log("Двери открываются");
            isDoorsOpen = true;
        }
    }
}