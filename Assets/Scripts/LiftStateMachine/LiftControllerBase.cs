using System;
using System.Collections;
using LiftStateMachine.Interactables;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftControllerBase : MonoBehaviour
    {
        [SerializeField] private GameObject liftBox;
        [SerializeField] private InnerDoors innerDoors;
        [SerializeField] private InnerPanel innerPanel;
        [SerializeField] private GameObject[] levels;
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private float liftSpeed = 1f;
        [SerializeField] private float holdTime;
        private Transform currentLevel;
        private Transform destinationLevel;
        private ILiftState _state;
        private Action _action;

        private void Awake()
        {
            liftControllerData.ResetData();
            liftControllerData.StartFSM();
            GetState();
        }
        
        private void FixedUpdate()
        {
            // механика движения лифта между уровнями
            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                if (liftControllerData.IsReadyToMove)
                {
                    if(liftBox.transform.position != destinationLevel.position)
                    {
                        var floorAt = liftBox.transform.position;
                        var floorTo = destinationLevel.position;
                        liftBox.transform.position = Vector3.MoveTowards(floorAt, floorTo, liftSpeed * Time.deltaTime);
                    }
                }
                // остановка
                if (liftBox.transform.position == destinationLevel.position)
                {
                    liftControllerData.IsReadyToMove = false;
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Idle();
                }
                // нажата кнопка STOP
                if (liftControllerData.IsStopped)
                {
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Stop();
                }
            }
        }

        private void Update()
        {
            UpdateState();
            currentLevel = levels[liftControllerData.CurrentFloor].transform;
            destinationLevel = levels[liftControllerData.DestinationFloor].transform;
            _action = liftControllerData.ActionFromData;
            if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
            {
                // поведение при вызове лифта по кнопке когда он уже на уровне
                if (liftControllerData.IsLiftCalled && IsOnLevel())
                {
                    EnterLevel();
                }
                // поведение лифта по прибытию на уровень
                if (IsOnLevel())
                {
                    if (!liftControllerData.IsReadyToMove && !liftControllerData.IsDoorsOpen)
                    {
                        EnterLevel();
                        if (liftControllerData.CurrentFloor != liftControllerData.DestinationFloor)
                        {
                            liftControllerData.CurrentFloor = liftControllerData.DestinationFloor;
                        }
                    }
                }
                // поведение при выборе уровня 
                if (liftControllerData.IsCodeEntered)
                {
                    if (liftControllerData.CurrentFloor != liftControllerData.DestinationFloor)
                    {
                        liftControllerData.IsCodeEntered = false;
                        liftControllerData.IsReadyToMove = true;
                        StartCoroutine(StartMoving());
                    }
                }
            }
            if (liftControllerData.CurrentState.GetType() == typeof(StopState))
            {
                if (!liftControllerData.IsStopped)
                {
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
                }
            }
        }

        IEnumerator StartMoving()
        {
            innerDoors.CloseDoors();
            yield return new WaitForSeconds(holdTime);
            liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
            liftControllerData.IsOnLevel = false;
            Debug.Log("END OF COROUTINE");
            StopCoroutine(StartMoving());
        }
        
        private bool IsOnLevel()
        {
            return currentLevel.position == levels[liftControllerData.CurrentFloor].transform.position;
        }
        
        private void EnterLevel()
        {
            innerDoors.OpenDoors();
            liftControllerData.IsLiftCalled = false;
            liftControllerData.IsOnLevel = true;
        }


        private void UpdateState()
        {
            if (!_state.Equals(liftControllerData.CurrentState))
            {
                _state.ExitState();
                GetState();
                _state.EnterState();
            }
        }

        private void GetState()
        {
            if (liftControllerData.CurrentState != null)
            {
                _state = liftControllerData.CurrentState;
            }
        }
    }
}