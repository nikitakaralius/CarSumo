using UnityEngine;
using Zenject;

namespace CarSumo.DataModel.GameResources
{
	[CreateAssetMenu(fileName = "GameResourceConsumption", menuName = "Game/GameResourceConsumption")]
	public class GameResourceConsumption : ScriptableObject, IResourceConsumption
	{
		[SerializeField, Min(0)] private int _gameEntryEnergy;
		
		private LazyInject<IResourceStorage> _storage;
		private LazyInject<IClientResourceOperations> _operations;
		
		[Inject]
		private void Initialize(LazyInject<IResourceStorage> storage, LazyInject<IClientResourceOperations> operations)
		{
			_storage = storage;
			_operations = operations;
		}

		public bool ConsumeIfEnoughToEnterGame()
		{
			int energyAmount = _storage.Value.GetResourceAmount(ResourceId.Energy).Value;
			bool enoughEnergy = energyAmount - _gameEntryEnergy >= 0;

			if (enoughEnergy)
				_operations.Value.TrySpend(ResourceId.Energy, _gameEntryEnergy);
			
			return enoughEnergy;
		}
	}
}