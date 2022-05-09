using System;
using LiftStateMachine.states;
using UnityEngine;
using UnityEngine.Events;

namespace LiftStateMachine
{
    [CreateAssetMenu(fileName = "LiftControllerData", menuName = "LiftController/LiftControllerData")]
    public class LiftControllerData : ScriptableObject
    {
        public string NextLevelCode { get; set; }

        public Transform CurrentLevel { get; set; }

        public Transform DestinationLevel { get; set; }

        public string EnteredCombination { get; set; }

        public bool IsOnLevel { get; set; }

        public bool IsCodeEntered { get; set; }

        public ILiftState CurrentState { get; set; }

        public LiftStateFactory StateFactory { get; set; }

        public bool IsDoorsOpen { get; set; }

        public bool IsLiftCalled { get; set; }

        public bool IsReadyToMove { get; set; }

        public bool IsStopped { get; set; }

        public Action ActionFromData { get; set; }

        public int CurrentFloor { get; set; }

        public int DestinationFloor { get; set; }

        public void ResetData()
        {
            IsDoorsOpen = false;
            IsLiftCalled = false;
            IsReadyToMove = false;
            IsStopped = false;
            IsCodeEntered = false;
            CurrentFloor = 0;
            DestinationFloor = 0;
        }

        public void StartFSM()
        {
            StateFactory = new LiftStateFactory(this);
            CurrentState = StateFactory.Idle();
            CurrentState.EnterState();
            Debug.Log("LIFT FSM STARTED");
        }
    }
}