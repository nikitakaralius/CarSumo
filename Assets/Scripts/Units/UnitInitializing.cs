﻿using System;
using System.Collections.Generic;
using CarSumo.Teams;
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

namespace CarSumo.Units
{
	public class UnitInitializing : SerializedMonoBehaviour
	{
		[SerializeField] private IReadOnlyDictionary<Team, IUnit[]> _units;

		private IVehicleAssetsProvider _assetsProvider;
		private IGameModePreferences _gameModePreferences;
		private IAsyncInstantiation _instantiation;
		
		[Inject]
		private void Construct(IVehicleAssetsProvider assetsProvider, IGameModePreferences gameModePreferences, IAsyncInstantiation instantiation)
		{
			_assetsProvider = assetsProvider;
			_gameModePreferences = gameModePreferences;
			_instantiation = instantiation;
		}
		
		private void OnEnable()
		{
			foreach (KeyValuePair<Team,IUnit[]> pair in _units)
			{
				CreateVehicles(pair.Key, pair.Value);
			}
		}

		private async void CreateVehicles(Team team, IReadOnlyList<IUnit> units)
		{
			IVehicleLayout layout = _gameModePreferences.GetAccountByTeam(team).Value.VehicleLayout;
			IReadOnlyReactiveCollection<VehicleId> layoutVehicles = layout.ActiveVehicles;
			
			if (units.Count != layoutVehicles.Count)
			{
				throw new InvalidOperationException("Еhe number of units does not match the number of vehicles");
			}
			
			for (var i = 0; i < layoutVehicles.Count; i++)
			{
				VehicleId id = layoutVehicles[i];
				AssetReferenceGameObject vehicleAsset = _assetsProvider.GetAssetByVehicleId(id);

				Vehicle vehicle = await _instantiation.InstantiateAsync<Vehicle>(vehicleAsset);
				units[i].InitializeVehicleBySelf(vehicle);
			}
		}
	}
}