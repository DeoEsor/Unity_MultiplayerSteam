using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            //Container.Bind<IPlayer>().To<Infrastructure.Player.Player>();
            //Container.Bind<Interface>().To<Implementation>().AsSingle();
            //Container.Bind<IManager>().ToSelf().As
        }
    }
}