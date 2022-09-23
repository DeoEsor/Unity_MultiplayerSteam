namespace Infrastructure.Core
{
    public interface IStateObject
    {
        IState State { get; set; }
    }
}