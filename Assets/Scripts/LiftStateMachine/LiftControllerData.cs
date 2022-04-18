using System;
using LiftStateMachine.states;
using UnityEngine;
using UnityEngine.Events;

namespace LiftStateMachine
{
    [CreateAssetMenu(fileName = "LiftControllerData", menuName = "LiftController/LiftControllerData")]
    public class LiftControllerData : ScriptableObject
    {
        private ILiftState _currentState;
        private LiftStateFactory _stateFactory;
        private bool isDoorsOpen;
        private bool isLiftCalled;
        private bool isReadyToMove;
        private bool isStopped;
        private Action actionFromData;
        private int currentFloor;
        private int destinationFloor;
        private Transform currentLevel;
        private Transform destinationLevel;
        private bool isOnLevel;
        private bool isCodeEntered;

        private string enteredCombination;

        public Transform CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        public Transform DestinationLevel
        {
            get => destinationLevel;
            set => destinationLevel = value;
        }

        public string EnteredCombination
        {
            get => enteredCombination;
            set => enteredCombination = value;
        }

        public bool IsOnLevel
        {
            get => isOnLevel;
            set => isOnLevel = value;
        }

        public bool IsCodeEntered
        {
            get => isCodeEntered;
            set => isCodeEntered = value;
        }

        public void ResetData()
        {
            isDoorsOpen = false;
            isLiftCalled = false;
            isReadyToMove = false;
            isStopped = false;
            isCodeEntered = false;
            currentFloor = 0;
            destinationFloor = 0;
        }

        public void StartFSM()
        {
            _stateFactory = new LiftStateFactory(this);
            _currentState = _stateFactory.Idle();
            _currentState.EnterState();
            Debug.Log("LIFT FSM STARTED");
        }

        public ILiftState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        public LiftStateFactory StateFactory
        {
            get => _stateFactory;
            set => _stateFactory = value;
        }

        public bool IsDoorsOpen
        {
            get => isDoorsOpen;
            set => isDoorsOpen = value;
        }

        public bool IsLiftCalled
        {
            get => isLiftCalled;
            set => isLiftCalled = value;
        }

        public bool IsReadyToMove
        {
            get => isReadyToMove;
            set => isReadyToMove = value;
        }

        public bool IsStopped
        {
            get => isStopped;
            set => isStopped = value;
        }

        public Action ActionFromData
        {
            get => actionFromData;
            set => actionFromData = value;
        }

        public int CurrentFloor
        {
            get => currentFloor;
            set => currentFloor = value;
        }

        public int DestinationFloor
        {
            get => destinationFloor;
            set => destinationFloor = value;
        }
    }
}