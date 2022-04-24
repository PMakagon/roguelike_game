using System;
using UnityEngine;
using UnityEngine.UI;

namespace LiftStateMachine
{
    public class DebugWindow : MonoBehaviour
    {
        [SerializeField] private LiftControllerData _liftControllerData;
        [SerializeField] private LevelChanger levelChanger;
        [SerializeField] private InnerPanel _panel;
        [SerializeField] private Text _currentState;
        [SerializeField] private Text isDoorsOpen;
        [SerializeField] private Text isLiftCalled;
        [SerializeField] public Text IsReadyToMove;
        [SerializeField] public Text IsStopped;
        [SerializeField] private Text currentFloor;
        [SerializeField] private Text destinationFloor;
        [SerializeField] private Text panelCommand;
        [SerializeField] private Text isOnLevel;
        [SerializeField] private Text isCodeEntered;
        [SerializeField] private Text EnteredCombination;
        [SerializeField] private Text nextLevelCode;
        
        private bool isActive = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tilde))
            {
                isActive=!isActive;
            }

            if (isActive)
            {
                PrintData();
            }
        }

        private void PrintData()
        {
            _currentState.text = "currentState " + _liftControllerData.CurrentState.GetType().Name;
            currentFloor.text = "currentFloor " +_liftControllerData.CurrentFloor.ToString();
            destinationFloor.text = "destinationFloor " +_liftControllerData.DestinationFloor.ToString();
            isDoorsOpen.text = "isDoorsOpen " +_liftControllerData.IsDoorsOpen.ToString();
            isLiftCalled.text ="isLiftCalled " + _liftControllerData.IsLiftCalled.ToString();
            IsReadyToMove.text ="IsReadyToMove " + _liftControllerData.IsReadyToMove.ToString();
            IsStopped.text = "IsStopped " +_liftControllerData.IsStopped.ToString();
            panelCommand.text = "panelCommand " + _panel._command.ToString();
            isOnLevel.text = "isOnLevel " + _liftControllerData.IsOnLevel.ToString();
            isCodeEntered.text = "isCodeEntered " + _liftControllerData.IsCodeEntered.ToString();
            nextLevelCode.text = "nextLevelCode " + levelChanger.NextLevelCode;
            EnteredCombination.text = "EnteredCombination " + _liftControllerData.EnteredCombination;
           
            
        }
    }
}