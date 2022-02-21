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
        private int _waitingTime = 10;
        private bool isDoorsOpen;
        private bool isLiftCalled;
        public bool isReadyToMove;
        public bool isStopped;
        private Action actionFromData;
        private int currentFloor;
        private int destinationFloor;

        public void ResetData()
        {
            isDoorsOpen = false;
            isLiftCalled = false;
            isReadyToMove = false;
            isStopped = false;
            currentFloor = 0;
            destinationFloor = 0;
        }

        public void StartFSM()
        {
            _stateFactory = new LiftStateFactory(this);
            _currentState = _stateFactory.Idle();
            _currentState.EnterState();
            Debug.Log("FSM STARTED");
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

        public int WaitingTime
        {
            get => _waitingTime;
            set => _waitingTime = value;
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