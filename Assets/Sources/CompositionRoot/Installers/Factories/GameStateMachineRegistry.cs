using System.Collections.Generic;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using Zenject;

namespace Infrastructure.Installers.Factories
{
    public class GameStateMachineRegistry : IFactory<GameStateMachine>
    {
        private readonly DiContainer _container;

        public GameStateMachineRegistry(DiContainer container)
        {
            _container = container;
        }

        public GameStateMachine Create()
        {
            return new GameStateMachine(RegisterStates());
        }

        private IEnumerable<IState> RegisterStates()
        {
            return new IState[]
            {
                _container.Instantiate<BootstrapState>(),
                _container.Instantiate<GameEntryState>(),
                _container.Instantiate<MenuEntryState>(),
                _container.Instantiate<MenuState>(),
                _container.Instantiate<GameState>(),
                _container.Instantiate<PauseState>(),
                _container.Instantiate<WinState>(),
                _container.Instantiate<AdvertisedMenuEntryState>()
            };
        }
    }
}