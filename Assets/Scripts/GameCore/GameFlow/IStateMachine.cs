namespace LiftGame.GameCore.GameFlow
{
    public interface IStateMachine
    {
        void Enter();
        void SetState(State state);
        void Exit();
        State GetState();
        bool IsActive { get; }
        bool IsFinal { get; }
    }
}