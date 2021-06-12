using System;
using CarSumo.Extensions;
using CarSumo.Units;
using CarSumo.Units.Stats;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Level
{
    public class VehicleDestroyer : SerializedMonoBehaviour
    {
        public event Action<IVehicleStatsProvider> VehicleDestroying;

        [SerializeField] private CinemachineImpulseSource _impulseSource;

        private void OnTriggerEnter(Collider other)
        {
            other.HandleComponent<Vehicle>(vehicle =>
            {
                _impulseSource.GenerateImpulse();
                VehicleDestroying?.Invoke(vehicle);
                vehicle.Destroy();
            });
        }
    }
}