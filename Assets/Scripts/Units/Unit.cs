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
            var factory = _hierarchy.ClampedGetVehicleFactoryByIndex(_generation);
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

        private void Upgrade(WorldPlacement worldPlacement)
        {
            Debug.Log($"{name} was upgraded");
            CreteVehicleInstance(worldPlacement);
        }
    }
}