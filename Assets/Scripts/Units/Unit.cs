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
        [SerializeField] private VehicleFactory _startVehicle;

        private void Awake()
        {
            CreteVehicleInstance(_startVehicle);
        }

        private void CreteVehicleInstance(VehicleFactory factory)
        {
            var vehicle = factory.Create(transform, _team);

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

        private void Upgrade()
        {
            Debug.Log($"{name} was upgraded");
        }
    }
}