using System;
using System.Linq;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class InnerPanel : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private Button[] buttons;

        public int _command = 500;
        public int _lastCommand;
        public bool buttonPressed;
        private string enteredCode;

        private void Awake()
        {
            enteredCode = string.Empty;
        }


        private void Update()
        {
            liftControllerData.EnteredCombination = enteredCode;
            if (enteredCode.Length >= 3)
            {
                foreach (var button in buttons)
                {
                    button.FlashIncorrect();
                }

                enteredCode = string.Empty;
            }

            if (liftControllerData.IsCodeEntered)
            {
                foreach (var button in buttons)
                {
                    button.FlashCorrect();
                    liftControllerData.IsReadyToMove = true;
                }
            }

            if (!buttonPressed)
            {
                _lastCommand = _command;
                // _command = 500;
            }

            if (buttonPressed)
            {
                buttonPressed = false;
                if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
                {
                    enteredCode += _command;

                    // if (_command == 0)
                    // {
                    //     liftControllerData.DestinationFloor = 0;
                    //     // liftControllerData.IsReadyToMove = true;
                    //     liftControllerData.IsCodeEntered = true;
                    //     buttonPressed = false;
                    // }
                    // if (_command == 1)
                    // {
                    //     liftControllerData.DestinationFloor = 1;
                    //     // liftControllerData.IsReadyToMove = true;
                    //     liftControllerData.IsCodeEntered = true;
                    //     buttonPressed = false;
                    // }
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