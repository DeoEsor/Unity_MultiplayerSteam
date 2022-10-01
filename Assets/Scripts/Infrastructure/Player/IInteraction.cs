namespace Infrastructure.Player
{
    public interface IInteraction<out TSelf>
    {
        TSelf GetInteractData();
        void InteractionRay();
    }
}