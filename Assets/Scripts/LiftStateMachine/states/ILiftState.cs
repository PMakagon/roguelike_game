namespace LiftStateMachine.states
{
    public interface ILiftState
    {
        void EnterState();
        void ExitState();
        void UpdateState();
    }
}