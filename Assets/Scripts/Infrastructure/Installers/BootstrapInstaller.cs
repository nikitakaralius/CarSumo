using CarSumo.Infrastructure.Factories;
using CarSumo.Infrastructure.SceneManagement;
using CarSumo.Infrastructure.StateMachine;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoadService();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .FromFactory<GameStateMachineRegistry>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .Bind<ISceneLoadService>()
                .FromInstance(new AddressablesSceneLoadService())
                .AsSingle()
                .NonLazy();
        }
    }
}