namespace Infrastructure.Core
{
    public interface IMoveControllable
    {
        IMoveControl MoveControl { get; set; }

        void MoveForward() => MoveControl.MoveForward();
        void MoveLeft() => MoveControl.MoveLeft();
        void MoveBackward() => MoveControl.MoveBackward();
        void MoveRight() => MoveControl.MoveRight();
    }
}