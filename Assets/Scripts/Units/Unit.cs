using System;
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

        private void Awake()
        {
            CreteVehicleInstance(new WorldPlacement(transform.position, -transform.forward));
        }

        private void CreteVehicleInstance(WorldPlacement worldPlacement)
        {
            _generation++;

            if (_hierarchy.TryGetVehicleFactoryByIndex(_generation, out var factory) == false)
                return;

            var vehicle = factory.Create(transform, _team);
            vehicle.SetWorldPlacement(worldPlacement);

            void DestroySelf()
            {
                Destroying?.Invoke(this);
                vehicle.Destroying -= DestroySelf;
                vehicle.Upgrading -= Upgrade;
                Destroy(gameObject);
            }

            vehicle.Destroying += DestroySelf;
            vehicle.Upgrading += Upgrade;
        }

        private void Upgrade(Vehicle vehicle)
        {
            if (_generation + 1 >= _hierarchy.Count)
                return;

            vehicle.Destroy(destroyWithUnit: false);
            CreteVehicleInstance(vehicle.WorldPlacement);
        }
    }
}