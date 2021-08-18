using CarSumo.Extensions;
using CarSumo.Vehicles;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Level
{
    public class VehicleDestroyer : SerializedMonoBehaviour
    {
	    [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private VehicleDestroyerAudio _audio;

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
                _audio.PlayExplosionSound(other);
                vehicle.Destroy();
            });
        }
    }
}