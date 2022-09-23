using Infrastructure.Core;

namespace Infrastructure.Environment
{
    public interface IInteractable
    {
        void Interactive<T>(IInteractive<T> interactable);
    }
}