using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace CarSumo.DataModel.GameResources
{
	[CreateAssetMenu(fileName = "GameResourceConsumption", menuName = "Game/GameResourceConsumption")]
	public class GameResourceConsumption : ScriptableObject, IResourceConsumption
	{
		[SerializeField, Min(0)] private int _gameEntryEnergy;

		private readonly Subject<bool> _enterGameConsumption = new Subject<bool>();

		private LazyInject<IClientResourceOperations> _operations;
		
		[Inject]
		private void Initialize(LazyInject<IClientResourceOperations> operations)
		{
			_operations = operations;
		}

		public bool ConsumeIfEnoughToEnterGame()
		{
			bool enoughMoney = _operations.Value.TrySpend(ResourceId.Energy, _gameEntryEnergy);
			_enterGameConsumption.OnNext(enoughMoney);
			return enoughMoney;
		}

		public IObservable<bool> ObserveEnterGameConsumption() => _enterGameConsumption;
	}
}