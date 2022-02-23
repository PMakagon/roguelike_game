using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine.Interactables
{
    public class InnerPanel : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        public int _command=500;
        public bool buttonPressed;


       
        private void Update()
        {
            if (!buttonPressed)
            {
                _command = 500;
            }
            if (buttonPressed)
            {
                if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
                {
                    if (_command == 0)
                    {
                        liftControllerData.DestinationFloor = 0;
                        // liftControllerData.IsReadyToMove = true;
                        liftControllerData.IsCodeEntered = true;
                        buttonPressed = false;
                    }
                    if (_command == 1)
                    {
                        liftControllerData.DestinationFloor = 1;
                        // liftControllerData.IsReadyToMove = true;
                        liftControllerData.IsCodeEntered = true;
                        buttonPressed = false;
                    }
                }
            
                if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
                {
                    if (_command == 111)
                    {
                        liftControllerData.IsReadyToMove = false;
                        liftControllerData.IsStopped = true;
                        buttonPressed = false;
                    }
                }
                
                if (liftControllerData.CurrentState.GetType() == typeof(StopState))
                {
                    if (_command == 111)
                    {
                        liftControllerData.IsReadyToMove = true;
                        liftControllerData.IsStopped = false;
                        buttonPressed = false;
                    }
                }
            }
        }
    }
}