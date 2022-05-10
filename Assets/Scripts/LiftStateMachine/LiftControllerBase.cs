using System;
using System.Collections;
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
        private Transform _currentLevel;
        private Transform _destinationLevel;
        private ILiftState _state;
        private Action _action;
        private LiftBoxLight _liftBoxLight;

        private void Awake()
        {
            liftControllerData.ResetData();
            liftControllerData.StartFSM();
            GetState();
            _liftBoxLight = GetComponentInChildren<LiftBoxLight>();
        }
        
        private void FixedUpdate()
        {
            // механика движения лифта между уровнями
            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                if (liftControllerData.IsReadyToMove)
                {
                    if(liftBox.transform.position != _destinationLevel.position)
                    {
                        var floorAt = liftBox.transform.position;
                        var floorTo = _destinationLevel.position;
                        liftBox.transform.position = Vector3.MoveTowards(floorAt, floorTo, liftSpeed * Time.deltaTime);
                    }
                }
            }
        }

        private void Update()
        {
            UpdateState();
            // currentLevel = levels[liftControllerData.CurrentFloor].transform;
            // destinationLevel = levels[liftControllerData.DestinationFloor].transform;
            _currentLevel = liftControllerData.CurrentLevel;
            _destinationLevel = liftControllerData.DestinationLevel;
            _action = liftControllerData.ActionFromData;//полная хуета убрать
            
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
                    }
                }
                
                if (liftControllerData.IsReadyToMove && liftControllerData.IsOnLevel)
                {
                    if (liftControllerData.CurrentLevel.position != liftControllerData.DestinationLevel.position)
                    {
                        liftControllerData.IsCodeEntered = false;
                        StartCoroutine(StartMoving());
                    }
                }
                
                // поведение при выборе уровня 
                if (liftControllerData.IsCodeEntered)
                {
                    liftControllerData.IsReadyToMove = true;
                }
            }

            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                // переместить в update?
                // остановка
                if (liftBox.transform.position == _destinationLevel.position)
                {
                    liftControllerData.IsReadyToMove = false;
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Idle();
                    liftControllerData.IsOnLevel = true;
                    // смена текущего уровня
                    if (liftControllerData.CurrentLevel.transform.position != liftControllerData.DestinationLevel.transform.position)
                    {
                        liftControllerData.CurrentLevel = liftControllerData.DestinationLevel;
                    }
                }
                // нажата кнопка STOP
                if (liftControllerData.IsStopped)
                {
                    liftControllerData.IsReadyToMove = false;
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Stop();
                }
            }

            if (liftControllerData.CurrentState.GetType() == typeof(StopState))
            {
                if (!liftControllerData.IsStopped)
                {
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
                    liftControllerData.IsReadyToMove = true;
                }
            }
        }

        private IEnumerator StartMoving()
        {
            innerDoors.CloseDoors();
            yield return new WaitForSeconds(holdTime);
            liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
            liftControllerData.IsOnLevel = false;
            // Debug.Log("END OF COROUTINE");
        }
        
        private bool IsOnLevel()
        {
            // return currentLevel.position == levels[liftControllerData.CurrentFloor].transform.position;
            return _currentLevel.position == liftControllerData.CurrentLevel.position;
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