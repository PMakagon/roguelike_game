using System;
using LiftGame.LiftStateMachine.states;
using UnityEngine;

namespace LiftGame.LiftStateMachine
{
    [CreateAssetMenu(fileName = "LiftControllerData", menuName = "LiftController/LiftControllerData")]
    public class LiftControllerData : ScriptableObject
    {
        public ILiftState CurrentState { get; set; } // текущее состояние лифта
        public LiftStateFactory StateFactory { get; set; } // фабрика состояний лифта
        public Transform CurrentLevel { get; set; } // позиция начала движения
        public Transform DestinationLevel { get; set; } // позиция конца движения
        public Transform StartLevel { get; set; } // позиция 0 этажа для начала движения и возвращения,впервые присваивается в LeveleChanger`e
        
        public string NextLevelCode { get; set; } // здесь хранится код уровня после генерации в LeveleChanger

        public string EnteredCombination { get; set; } // то что ввёл игрок в панель лифта

        public bool IsOnLevel { get; set; } // лифт находится на уровне. Активируется при входе на уровень

        public bool IsOnStartLevel { get; set; } // лифт находится на уровне.Активируется при входе на уровень

        public bool IsPlayerInside { get; set; } // игрок в кабине лифта

        public bool IsPlayerLeft { get; set; } // игрок покинул зону лифта

        public bool IsCodeEntered { get; set; } // игрок правильно ввёл код. Приходит от LevelChanger
        
        public bool IsDoorsOpen { get; set; } // открыты\закрыты ли двери НА момент проверки

        public bool IsLiftCalled { get; set; } // прожата ли кнопка вызова лифта

        public bool IsReadyToMove { get; set; } // Команда начала движения. В movingState активирует движение. 

        public bool IsStopped { get; set; } // лифт в состоянии Остановки

        public Action OnDoorsAction { get; set; } // событие на открытие закрытие дверей в зависимости от их состояния
        
        public  Action OnCodeEntered { get; set; } // событие при вводе верного кода( для генератора)
        
        public Action OnPlayerLeftLiftZone { get; set; } // TEST
        
        public Action OnGoingBackToStartLevel { get; set; } // TEST
        
        public static Action OnLevelGameLoopFinished { get; set; } // игрок успешно вернулся в хаб
        
        public static Action OnPlayerEnteredNewLevel { get; set; } // Лифт привез игрока на новый уровень
        
        public static Action OnLiftCalled { get; set; } // вызов лифта по кнопке
        
        
        


        public void ResetData() // вызывается контроллером лифта при запуске
        {
            IsDoorsOpen = false;
            IsLiftCalled = false;
            IsReadyToMove = false;
            IsStopped = false;
            IsCodeEntered = false;
            IsPlayerInside = false;
            IsOnStartLevel = true;
            IsOnLevel = false;
        }

        public void StartFSM()
        {
            StateFactory = new LiftStateFactory(this);
            CurrentState = StateFactory.Idle();
            CurrentState.EnterState();
            // Debug.Log("LIFT FSM STARTED");
        }
    }
}