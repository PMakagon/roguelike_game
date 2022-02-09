using LiftStateMachine.states;

namespace LiftStateMachine
{
    public class LiftStateFactory
    {
        private LiftControllerBase _context;

        public LiftStateFactory(LiftControllerBase currentContext)
        {
            _context = currentContext;
        }

        public ILiftState Idle()
        {
            return new IdleState();
        }

        public ILiftState Moving()
        {
            return new MovingState();
        }


        public ILiftState Stop()
        {
            return new StopState();
        }

    }
}