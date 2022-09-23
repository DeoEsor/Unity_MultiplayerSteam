namespace Infrastructure.Core
{
    public interface IMoveControl
    {
        void MoveForward();
        void MoveLeft();
        void MoveBackward();
        void MoveRight();
    }
}