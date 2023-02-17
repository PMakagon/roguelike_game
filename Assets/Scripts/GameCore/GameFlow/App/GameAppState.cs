using LiftGame.GameCore.GameFlow.Game;

namespace LiftGame.GameCore.GameFlow.App
{
    public class GameAppState : State
    {
        private GameStateMachine _gameStateMachine;
        public GameAppState(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
            StateMachine.Exit();
            _gameStateMachine ??= new GameStateMachine();
            _gameStateMachine.Enter();
        }

        public override void OnExit()
        {
            _gameStateMachine.Exit();
            StateMachine.Enter();
        }
    }
}