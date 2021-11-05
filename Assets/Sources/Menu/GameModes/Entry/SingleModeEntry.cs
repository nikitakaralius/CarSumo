using System.Linq;
using AI.Repositories;
using CarSumo.Teams;
using GameModes;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.Entry
{
	public class SingleModeEntry : MonoBehaviour
	{
		[SerializeField] private MenuGameEntry _entry;

		private IGameModeOperations _operations;

		[Inject]
		private void Construct(IGameModeOperations operations)
		{
			_operations = operations;
		}
		
		public void TryEnterGame()
		{
			_operations.RegisterAccount(Team.Red, new AIAccountRepository().Accounts.First());
			_entry.TryEnterGame();
		}
	}
}