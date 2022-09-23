using Infrastructure.Environment;

namespace Infrastructure.Core
{
    public interface IInteractive<out TSelf>
    {
        TSelf GetInteractiveData();

        void Interactive(IInteractable interactable);
    }
}