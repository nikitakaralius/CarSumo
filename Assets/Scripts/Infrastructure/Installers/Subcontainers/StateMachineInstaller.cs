using CarSumo.StateMachine;
using Infrastructure.Installers.Factories;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            BindStateMachine();
        }

        private void BindStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .FromFactory<GameStateMachineRegistry>()
                .AsSingle()
                .NonLazy();
        }
    }
}