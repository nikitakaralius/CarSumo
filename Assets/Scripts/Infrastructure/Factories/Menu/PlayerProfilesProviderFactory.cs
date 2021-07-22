using CarSumo.Menu.Models;
using CarSumo.Players.Models;
using DataManagement.Players.Services;
using Zenject;

namespace CarSumo.Infrastructure.Factories.Menu
{
    public class PlayerProfilesProviderFactory : IFactory<IPlayerProfilesProvider>
    {
        private readonly IPlayerProfileBuilder _builder;
        private readonly IPlayerProfilesUpdate _update;
        private readonly PlayersDataService _dataService;

        public PlayerProfilesProviderFactory(IPlayerProfileBuilder builder,
                                             IPlayerProfilesUpdate update,
                                             PlayersDataService dataService)
        {
            _builder = builder;
            _update = update;
            _dataService = dataService;
        }
        
        public IPlayerProfilesProvider Create()
        {
            var binder = new PlayerProfilesBinder(_builder, _dataService, _update);
            binder.Bind();
            return binder;
        }
    }
}