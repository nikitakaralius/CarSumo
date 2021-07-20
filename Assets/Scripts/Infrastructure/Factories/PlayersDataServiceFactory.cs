using CarSumo.DataManagement.Core;
using DataManagement.Players.Services;

namespace CarSumo.Infrastructure.Factories
{
    public class PlayersDataServiceFactory : DataFactory<PlayersDataService>
    {
        public PlayersDataServiceFactory(IFileService fileService) : base(fileService)
        {
        }

        public override PlayersDataService Create()
        {
            return new PlayersDataService(FileService, SettingsDirectory);
        }
    }
}