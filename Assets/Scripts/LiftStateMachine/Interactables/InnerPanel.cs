using System;
using System.Collections;
using System.Collections.Generic;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class InnerPanel : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private Button[] buttons;
        [SerializeField] private StartButton startButton;
        [SerializeField] private StopButton stopButton;
        private int _command = 500;
        private int _lastCommand;

        public bool buttonPressed;

        private string enteredCode;

        public string EnteredCode => enteredCode;

        public List<Button> CurrentSelection { get; set; }

        private IEnumerator FlashIncorrect()
        {
            foreach (var button in buttons)
            {
                button.TurnLightOn();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in buttons)
            {
                button.TurnLightOff();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in buttons)
            {
                button.TurnLightOn();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in buttons)
            {
                button.TurnLightOff();
            }
        }

        private IEnumerator FlashCorrect()
        {
            foreach (var button in CurrentSelection)
            {
                button.TurnLightOff();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in CurrentSelection)
            {
                button.TurnLightOn();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in CurrentSelection)
            {
                button.TurnLightOff();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in CurrentSelection)
            {
                button.TurnLightOn();
            }
            yield return new WaitForSeconds(0.5f);
            foreach (var button in CurrentSelection)
            {
                button.TurnLightOff();
            }
            Debug.Log("DONE");
        }

        private void Awake()
        {
            enteredCode = string.Empty;
            CurrentSelection = new List<Button>();
            liftControllerData.EnteredCombination = enteredCode;
        }

        private void Update()
        { 
            liftControllerData.EnteredCombination = enteredCode;
            if (enteredCode.Length > liftControllerData.NextLevelCode.Length)
            {
                StartCoroutine(FlashIncorrect());
                enteredCode = string.Empty;
                Debug.Log("WRONG CODE");
            }

            if (startButton.StartPressed)
            {
                if (liftControllerData.NextLevelCode.Equals(enteredCode))
                {
                    StartCoroutine(FlashCorrect());
                    liftControllerData.IsCodeEntered = true;
                    Debug.Log("CORRECT CODE");
                }
                else
                {
                    if (enteredCode.Equals(1.ToString()))
                    {
                        liftControllerData.DestinationLevel = liftControllerData.StartLevel;
                        liftControllerData.IsReadyToMove = true;
                        Debug.Log("HOME");
                    }
                    else
                    {
                        StartCoroutine(FlashIncorrect());
                        Debug.Log("WRONG CODE");
                    }
                }
                enteredCode = string.Empty;
                CurrentSelection.Clear();
                startButton.StartPressed = false;
            }

            if (!buttonPressed)
            {
                _lastCommand = _command;
            }

            if (buttonPressed)
            {
                buttonPressed = false;
                if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
                {
                    enteredCode += _command;
                }
            }
            
            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                if (stopButton.StopPressed)
                {
                    liftControllerData.IsStopped = true;
                }

                if (startButton.StartPressed)
                {
                    StartCoroutine(FlashIncorrect());
                }
            }

            if (liftControllerData.CurrentState.GetType() == typeof(StopState))
            {
                if (!stopButton.StopPressed)
                {
                    liftControllerData.IsStopped = false;
                }
            }
        }

        public int Command
        {
            get => _command;
            set => _command = value;
        }

        public int LastCommand
        {
            get => _lastCommand;
        }
    }
}