using System;

namespace LiftGame.GameCore.GameFlow.App
{
    public class BootState : State
    {
        public static event Action OnBootFinish;
        public BootState(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
           
        }

        public override void OnExit()
        {
           StateMachine.SetState(new MenuAppState(StateMachine));
           OnBootFinish?.Invoke();
        }
    }
}