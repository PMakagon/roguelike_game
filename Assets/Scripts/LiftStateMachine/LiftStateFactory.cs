using LiftGame.LiftStateMachine.states;

namespace LiftGame.LiftStateMachine
{
    public class LiftStateFactory
    {
        private LiftControllerData _context;

        public LiftStateFactory(LiftControllerData currentContext)
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