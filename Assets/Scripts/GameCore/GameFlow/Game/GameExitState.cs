namespace LiftGame.GameCore.GameFlow.Game
{
    public class GameExitState : State
    {
        public GameExitState(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
           StateMachine.Enter();
        }

        public override void OnExit()
        {
           
        }
    }
}