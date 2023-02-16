namespace LiftGame.GameCore.GameFlow
{
    public abstract class State
    {
        public IStateMachine StateMachine { get; }

        protected State(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}