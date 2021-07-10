using CarSumo.Infrastructure.Factories;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.SceneManagement;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSingleSceneLoader();
            BindAdditiveSceneLoader();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .FromFactory<GameStateMachineRegistry>()
                .AsSingle();
        }

        private void BindAdditiveSceneLoader()
        {
            Container
                .Bind<AdditiveSceneLoader>()
                .FromNew()
                .AsSingle();
        }

        private void BindSingleSceneLoader()
        {
            Container
                .Bind<SingleSceneLoader>()
                .FromNew()
                .AsSingle();
        }
    }
}