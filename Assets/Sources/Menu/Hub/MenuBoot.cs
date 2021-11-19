using GameModes;
using GameModes.Extensions;
using UnityEngine;
using Zenject;

namespace Menu.Hub
{
	public class MenuBoot : MonoBehaviour
	{
		private IGameModeOperations _operations;

		[Inject]
		private void Construct(IGameModeOperations operations)
		{
			_operations = operations;
		}

		private void OnEnable()
		{
			_operations.ClearRegistrations();
		}
	}
}