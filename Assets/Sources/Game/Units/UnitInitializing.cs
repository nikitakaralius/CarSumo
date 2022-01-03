using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using GameModes;
using Services.Instantiate;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Vehicle = DataModel.Vehicles.Vehicle;

namespace CarSumo.Units
{
	public class UnitInitializing : SerializedMonoBehaviour
	{
		[SerializeField] private IReadOnlyDictionary<Team, IUnit[]> _units = new Dictionary<Team, IUnit[]>();

		private IVehicleAssets _assets;
		private IGameModePreferences _gameModePreferences;
		private IAsyncInstantiation _instantiation;
		
		[Inject]
		private void Construct(IVehicleAssets assets,
								IGameModePreferences gameModePreferences,
								IAsyncInstantiation instantiation)
		{
			_assets = assets;
			_gameModePreferences = gameModePreferences;
			_instantiation = instantiation;
		}

		public async Task InitializeAsync()
		{
			foreach (KeyValuePair<Team,IUnit[]> pair in _units)
			{
				await CreateVehicles(pair.Key, pair.Value);
			}
		}
		
		private async Task CreateVehicles(Team team, IReadOnlyList<IUnit> units)
		{
			IVehicleDeck deck = _gameModePreferences.GetAccountByTeam(team).Value.VehicleDeck;
			IReadOnlyReactiveCollection<Vehicle> layoutVehicles = deck.ActiveVehicles;
			
			if (units.Count != layoutVehicles.Count)
			{
				throw new InvalidOperationException("The number of units does not match the number of vehicles");
			}
			
			for (var i = 0; i < layoutVehicles.Count; i++)
			{
				Vehicle id = layoutVehicles[i];
				AssetReferenceGameObject vehicleAsset = _assets.GetAssetByVehicleId(id);

				Vehicles.Vehicle vehicle = await _instantiation.InstantiateAsync<Vehicles.Vehicle>(vehicleAsset);
				units[i].InitializeVehicleBySelf(vehicle);
			}
		}
	}
}