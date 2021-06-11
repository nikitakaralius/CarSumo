using System;
using CarSumo.Extensions;
using CarSumo.Units;
using CarSumo.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Level
{
    public class VehicleDestroyer : SerializedMonoBehaviour
    {
        public event Action<IVehicleStatsProvider> VehicleDestroying;

        private void OnTriggerEnter(Collider other)
        {
            other.HandleComponent<Vehicle>(vehicle =>
            {
                VehicleDestroying?.Invoke(vehicle);
                vehicle.Destroy();
            });
        }
    }
}