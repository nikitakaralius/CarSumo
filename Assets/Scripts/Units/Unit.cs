using CarSumo.Teams;
using CarSumo.Units.Factory;
using CarSumo.Units.Stats;
using UnityEngine;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IVehicleStatsProvider
    {
        [SerializeField] private Team _team;
        [SerializeField] private VehicleFactory _startVehicle;

        private void Start()
        {
            CreteVehicleInstance(_startVehicle);      
        }

        public VehicleStats GetStats()
        {
            return new VehicleStats {Team = _team};
        }

        private void CreteVehicleInstance(VehicleFactory factory)
        {
            var vehicle = factory.Create(transform, this);

            void DestroySelf()
            {
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