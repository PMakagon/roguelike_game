using System.Collections;
using LiftGame.LiftStateMachine.states;
using UnityEngine;

namespace LiftGame.LiftStateMachine
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private Transform liftBox; // заменить на transform
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private float liftSpeed = 1f;
        [SerializeField] private float holdTime = 1f;
        private Transform _currentLevel;
        private Transform _destinationLevel;
        private ILiftState _state;
        private LiftBoxLight _liftBoxLight;

        private void Awake()
        {
            liftControllerData.ResetData();
            liftControllerData.StartFSM();
            GetState();
            // _liftBoxLight = GetComponentInChildren<LiftBoxLight>(); // непонятно зачем
            liftControllerData.OnPlayerLeftLiftZone += GoBackToStartLevel; // TEST
            liftControllerData.OnGoingBackToStartLevel += GoBackToStartLevel;
            LiftControllerData.OnLiftCalled += ReactOnLiftCall;
        }

        private void OnDisable()
        {
            liftControllerData.OnPlayerLeftLiftZone -= GoBackToStartLevel;
            liftControllerData.OnGoingBackToStartLevel -= GoBackToStartLevel;
            LiftControllerData.OnLiftCalled -= ReactOnLiftCall;
        }

        private void FixedUpdate()
        {
            // механика движения лифта между уровнями
            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                if (liftControllerData.IsReadyToMove)
                {
                    if (liftBox.position != _destinationLevel.position)
                    {
                        var liftPosition = liftBox.position;
                        var levelPosition = _destinationLevel.position;
                        liftBox.position = Vector3.MoveTowards(liftPosition, levelPosition, liftSpeed * Time.deltaTime);
                    }
                }
            }
        }

        private void ReactOnLiftCall()
        {
            if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
            {
                // поведение при вызове лифта по кнопке 
                if (liftControllerData.IsOnStartLevel && !liftControllerData.IsPlayerLeft)
                {
                    EnterStartLevel();
                }
                else
                {
                    GoToCurrentLevel();
                }
            }
        }

        private void Update()
        {
            UpdateState();

            _currentLevel = liftControllerData.CurrentLevel;
            _destinationLevel = liftControllerData.DestinationLevel;

            if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
            {
                // // поведение при вызове лифта по кнопке 
                // if (liftControllerData.IsLiftCalled)
                // {
                //     if (liftControllerData.IsOnStartLevel && !liftControllerData.IsPlayerLeft)
                //     {
                //         EnterStartLevel();
                //     }
                //     else
                //     {
                //         GoToCurrentLevel();
                //     }
                //     liftControllerData.IsLiftCalled = false;
                // }

                // Debug.Log(IsOnStartLevel());
                // Debug.Log(IsOnCurrentLevel());

                /// лифт по прибытию на стартовый уровень
                if (!liftControllerData.IsOnStartLevel && IsOnStartLevel())
                {
                    EnterStartLevel();
                }

                // поведение лифта по прибытию на уровень
                if (!liftControllerData.IsOnLevel && IsOnDestinationLevel())
                {
                    if (!liftControllerData.IsReadyToMove)
                    {
                        EnterLevel();
                    }
                }

                //после подтверждения кода  // пересмотреть
                if (liftControllerData.IsReadyToMove && liftControllerData.IsCodeEntered)
                {
                    if (liftControllerData.CurrentLevel.position != liftControllerData.DestinationLevel.position)
                    {
                        liftControllerData.IsCodeEntered = false;
                        StartCoroutine(StartMoving());
                    }
                }

                // введен вверный код 
                if (liftControllerData.IsCodeEntered)
                {
                    liftControllerData.IsReadyToMove = true;
                }
            }

            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                // остановка на выбранном уровне
                if (IsOnDestinationLevel())
                {
                    liftControllerData.IsReadyToMove = false;
                    liftControllerData.CurrentState = liftControllerData.StateFactory.Idle();
                    // проверяем где мы
                    if (IsOnStartLevel())
                    {
                        // если мы на стартовом уровне значит 
                        // а - игрок в кабине и приехал в хаб и можно заменить CurrentLevel на StartLevel
                        if (liftControllerData.IsPlayerInside)
                        {
                            liftControllerData.CurrentLevel = liftControllerData.StartLevel;
                            // LiftControllerData.OnLevelGameLoopFinished.Invoke();
                        }
                        // б - игрок на уровне и может вызвать лифт обратно
                        // ничего не делаем
                    }
                    else // не на стартовом уровне и можно заменить CurrentLevel // СТАРЫЙ ВАРИАНТ 
                    {
                        if (liftControllerData.CurrentLevel.position != liftControllerData.DestinationLevel.position)
                        {
                            liftControllerData.CurrentLevel = liftControllerData.DestinationLevel;
                        }
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
            if (liftControllerData.IsDoorsOpen)
            {
                liftControllerData.IsDoorsOpen = false;
                liftControllerData.OnDoorsAction.Invoke();
            }
            yield return new WaitForSeconds(holdTime);
            liftControllerData.IsStopped = false; // TEST затычка чтобы нельзя было застрять на уровне
            liftControllerData.CurrentState = liftControllerData.StateFactory.Moving();
            liftControllerData.IsOnLevel = false;
        }

        private void GoBackToStartLevel()
        {
            liftControllerData.DestinationLevel = liftControllerData.StartLevel;
            liftControllerData.IsReadyToMove = true;
            StartCoroutine(StartMoving());
        }

        private void GoToCurrentLevel()
        {
            liftControllerData.DestinationLevel = liftControllerData.CurrentLevel;
            liftControllerData.IsReadyToMove = true;
            StartCoroutine(StartMoving());
        }

        private bool IsOnDestinationLevel() //проверка присутствия лифта на точке назначения
        {
            return liftBox.position == _destinationLevel.position;
            // return liftControllerData.DestinationLevel.position == liftControllerData.CurrentLevel.position;
        }

        private bool IsOnStartLevel()
        {
            return liftBox.position == liftControllerData.StartLevel.position;
        }

        private void EnterLevel()
        {
            if (liftControllerData.IsPlayerInside || liftControllerData.IsLiftCalled)
            {
                liftControllerData.IsDoorsOpen = true;
                liftControllerData.OnDoorsAction.Invoke();
                liftControllerData.IsLiftCalled = false;
            }

            liftControllerData.IsOnLevel = true;
            Debug.Log("Entered Current Level");
        }

        private void EnterStartLevel()
        {
            if (liftControllerData.IsPlayerInside || liftControllerData.IsLiftCalled)
            {
                liftControllerData.IsDoorsOpen = true;
                liftControllerData.OnDoorsAction.Invoke();
                liftControllerData.IsLiftCalled = false;
            }
            liftControllerData.IsOnStartLevel = true;
            Debug.Log("Entered Start Level");
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