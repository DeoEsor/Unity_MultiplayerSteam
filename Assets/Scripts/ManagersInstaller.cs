using Player;
using Zenject;

public class ManagersInstaller : MonoInstaller<ManagersInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().ToSelf().Lazy();
    }
}