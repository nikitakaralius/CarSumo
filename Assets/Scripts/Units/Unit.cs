﻿using System;
using CarSumo.Teams;
using CarSumo.Units.Factory;
using UnityEngine;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour
    {
        public event Action<Unit> Destroying;

        public Team Team => _team;

        [SerializeField] private Team _team;
        [SerializeField] private VehicleHierarchyFactory _hierarchy;

        private int _generation = -1;

        private Vehicle _controlledVehicle;

        private void Awake()
        {
            /*todo: check -transform.forward in case of changing vehicle assets.
            In current pack vehicles have broken forward vector which directed backwards instead*/

            var unitWorldPlacement = new WorldPlacement(transform.position, -transform.forward);
            CreteVehicleInstance(unitWorldPlacement);
        }

        private void OnDisable()
        {
            _controlledVehicle.Destroying -= DestroyUnitInstance;
            _controlledVehicle.Upgrading -= UpgradeVehicle;
        }

        private void CreteVehicleInstance(WorldPlacement worldPlacement)
        {
            _generation++;

            if (_hierarchy.TryGetVehicleFactoryByIndex(_generation, out var factory) == false)
                return;

            _controlledVehicle = factory.Create(transform, _team);
            _controlledVehicle.SetWorldPlacement(worldPlacement);

            _controlledVehicle.Destroying += DestroyUnitInstance;
            _controlledVehicle.Upgrading += UpgradeVehicle;
        }

        private void UpgradeVehicle()
        {
            if (_generation + 1 >= _hierarchy.Count)
                return;

            var worldPlacement = _controlledVehicle.WorldPlacement;
            _controlledVehicle.DestroyWithoutNotification();
            CreteVehicleInstance(worldPlacement);
        }

        private void DestroyUnitInstance()
        {
            Destroying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}