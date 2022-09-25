using Infrastructure;
using Zenject;

namespace Core
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            //Container.Bind<Interface>().To<Implementation>().AsSingle();
            //Container.Bind<IManager>().ToSelf().As
        }
    }
}