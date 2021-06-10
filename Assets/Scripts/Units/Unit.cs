using CarSumo.Teams;
using CarSumo.Units.Factory;
using UnityEngine;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private VehicleFactory _startVehicle;

        private void Start()
        {
            CreteVehicleInstance(_startVehicle);
        }

        private void CreteVehicleInstance(VehicleFactory factory)
        {
            var vehicle = factory.Create(transform, _team);

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