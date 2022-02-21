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
        [SerializeField] private int holdTime = 3;
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
                if (!liftControllerData.IsDoorsOpen)
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
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Idle();
                }

                if (liftControllerData.isStopped)
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
                if (liftControllerData.IsLiftCalled && currentLevel.position ==
                    levels[liftControllerData.CurrentFloor].transform.position)
                {
                    EnterLevel();
                }
                // поведение лифта по прибытию на уровень
                if (currentLevel.position == levels[liftControllerData.CurrentFloor].transform.position)
                {
                    if (!liftControllerData.IsDoorsOpen)
                    {
                        EnterLevel();
                        if (liftControllerData.CurrentFloor != liftControllerData.DestinationFloor)
                        {
                            liftControllerData.CurrentFloor = liftControllerData.DestinationFloor;
                        }
                    }
                }
                // поведение при выборе уровня 
                if (liftControllerData.IsReadyToMove)
                {
                    liftControllerData.IsReadyToMove = false;
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
                    innerDoors.ActivateDoors();
                    
                }
            }

            if (liftControllerData.CurrentState.GetType() == typeof(StopState))
            {
                if (!liftControllerData.isStopped)
                {
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
                }
            }
        }

        // private IEnumerator Movement()
        // {
        //     yield return new WaitForSeconds(holdTime);
        //     Debug.Log(liftBox.transform.position == destinationLevel.position);
        //     Debug.Log(currentLevel.position);
        //     while (liftBox.transform.position != destinationLevel.position) yield return null;
        //     {
        //         var floorAt = liftBox.transform.position;
        //         var floorTo = destinationLevel.position;
        //         liftBox.transform.position = Vector3.MoveTowards(floorAt, floorTo, liftSpeed * Time.deltaTime);
        //     }
        //     if (liftBox.transform.position == destinationLevel.position)
        //     {
        //         liftControllerData.CurrentState = liftControllerData.StateFactory.Idle();
        //     }
        //
        //     yield return new WaitForSeconds(holdTime);
        //     EnterLevel();
        //     yield return null;
        // }

        private void EnterLevel()
        {
            innerDoors.OpenDoors();
            liftControllerData.IsLiftCalled = false;
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