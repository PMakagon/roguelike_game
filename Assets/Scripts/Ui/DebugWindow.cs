using System;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UI;

namespace LiftStateMachine
{
    public class DebugWindow : MonoBehaviour
    {
        [SerializeField] private LiftControllerData _liftControllerData;
        [SerializeField] private GameObject debugPanel;
        [SerializeField] private LevelChanger levelChanger;
        [SerializeField] private InnerPanel _panel;
        [SerializeField] private Text _currentState;
        [SerializeField] private Text isDoorsOpen;
        [SerializeField] private Text isLiftCalled;
        [SerializeField] public Text IsReadyToMove;
        [SerializeField] public Text IsStopped;
        [SerializeField] private Text isOnLevel;
        [SerializeField] private Text isCodeEntered;
        [SerializeField] private Text EnteredCombination;
        [SerializeField] private Text nextLevelCode;
        
        private bool _isActive;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                _isActive=!_isActive;
                debugPanel.SetActive(_isActive);
            }

            if (_isActive)
            {
                PrintData();
            }
        }

        private void PrintData()
        {
            _currentState.text = "currentState " + _liftControllerData.CurrentState.GetType().Name;
            isDoorsOpen.text = "isDoorsOpen " +_liftControllerData.IsDoorsOpen.ToString();
            isLiftCalled.text ="isLiftCalled " + _liftControllerData.IsLiftCalled.ToString();
            IsReadyToMove.text ="IsReadyToMove " + _liftControllerData.IsReadyToMove.ToString();
            IsStopped.text = "IsStopped " +_liftControllerData.IsStopped.ToString();
            isOnLevel.text = "isOnLevel " + _liftControllerData.IsOnLevel.ToString();
            isCodeEntered.text = "isCodeEntered " + _liftControllerData.IsCodeEntered.ToString();
            nextLevelCode.text = "nextLevelCode " + levelChanger.NextLevelCode;
            EnteredCombination.text = "EnteredCombination " + _liftControllerData.EnteredCombination;
            
        }
    }
}