using System;
using CarSumo.Extensions;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Stats;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Level
{
    public class VehicleDestroyer : SerializedMonoBehaviour
    {
        public event Action<IVehicleStatsProvider> VehicleDestroying;

        [SerializeField] private CinemachineImpulseSource _impulseSource;

        private Vehicle _previousVehicle;

        private void OnTriggerEnter(Collider other)
        {
            other.HandleComponent<Vehicle>(vehicle =>
            {
                //OnTriggerEnter sometimes calls twice. This check was made to prevent such situations
                if (vehicle == _previousVehicle)
                    return;

                _previousVehicle = vehicle;

                _impulseSource.GenerateImpulse();
                VehicleDestroying?.Invoke(vehicle);
                vehicle.Destroy();
            });
        }
    }
}