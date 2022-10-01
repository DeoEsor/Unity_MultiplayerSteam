using Player;
using Zenject;

namespace Core
{
    public class ManagersInstaller : MonoInstaller<ManagersInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerManager>().ToSelf().Lazy();
        }
    }
}