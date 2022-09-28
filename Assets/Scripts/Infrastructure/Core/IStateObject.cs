namespace Infrastructure.Core
{
    public interface IStateObject
    {
        IStateMachine CurrentState { get; set; }

        void Initialize(IStateMachine startingState);

        void ChangeState(IStateMachine newState);
    }
}