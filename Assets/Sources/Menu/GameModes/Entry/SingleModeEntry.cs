using AI.Repositories;
using CarSumo.Extensions;
using CarSumo.Teams;
using GameModes;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.Entry
{
	public class SingleModeEntry : MonoBehaviour
	{
		[SerializeField] private MenuGameEntry _entry;
		[SerializeField] private AIAccountRepository _repository;

		private IGameModeOperations _operations;

		[Inject]
		private void Construct(IGameModeOperations operations)
		{
			_operations = operations;
		}
		
		public void TryEnterGame()
		{
			_operations.RegisterAccount(Team.Red, _repository.Accounts.Random());
			_entry.TryEnterGame();
		}
	}
}