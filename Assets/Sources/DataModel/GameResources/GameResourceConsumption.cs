using UnityEngine;
using Zenject;

namespace CarSumo.DataModel.GameResources
{
	[CreateAssetMenu(fileName = "GameResourceConsumption", menuName = "Game/GameResourceConsumption")]
	public class GameResourceConsumption : ScriptableObject, IResourceConsumption
	{
		[SerializeField, Min(0)] private int _gameEntryEnergy;
		
		private IResourceStorage _storage;
		private IClientResourceOperations _operations;

		[Inject]
		private void Construct(IClientResourceOperations operations)
		{
			_operations = operations;
		}

		public bool ConsumeIfEnoughToEnterGame()
		{
			int energyAmount = _storage.GetResourceAmount(ResourceId.Energy).Value;
			bool enoughEnergy = energyAmount - _gameEntryEnergy >= 0;

			if (enoughEnergy)
				_operations.TrySpend(ResourceId.Energy, _gameEntryEnergy);
			
			return enoughEnergy;
		}
	}
}