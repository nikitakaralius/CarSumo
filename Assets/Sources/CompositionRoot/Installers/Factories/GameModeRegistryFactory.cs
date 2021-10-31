using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using GameModes;
using Zenject;

namespace Infrastructure.Installers.Factories
{
	public class GameModeRegistryFactory : IFactory<GameModeRegistry>
	{
		private readonly IAccountStorage _accountStorage;

		public GameModeRegistryFactory(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}
		
		public GameModeRegistry Create()
		{
			return new GameModeRegistry(RegisterInitialAccounts());
		}

		private IReadOnlyDictionary<Team, Account> RegisterInitialAccounts()
		{
			Account blueTeamAccount = _accountStorage.ActiveAccount.Value;
			Account redTeamAccount = _accountStorage.AllAccounts
				.FirstOrDefault(account => account.Equals(blueTeamAccount) == false);
			
			return new Dictionary<Team, Account>
			{
				{Team.Blue, blueTeamAccount},
				{Team.Red, redTeamAccount}
			};
		}
	}
}