using Infrastructure.Core;

namespace Core.Core
{
    public class PlayerStateObject : IStateObject
    {
        public IStateMachine CurrentState { get; set; }
        
        public void Initialize(IStateMachine startingState)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeState(IStateMachine newState)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeState(byte newStateFlag)
        {
            throw new System.NotImplementedException();
        }
    }
}