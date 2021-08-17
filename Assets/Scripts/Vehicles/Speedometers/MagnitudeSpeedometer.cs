using CarSumo.Calculations;
using CarSumo.Coroutines;
using System;
using System.Collections;
using UnityEngine;

namespace CarSumo.Vehicles.Speedometers
{
    public class MagnitudeSpeedometer : IVehicleSpeedometer
    {
        public float PowerPercentage { get; private set; }

        public MagnitudeSpeedometer(Rigidbody rigidbody, CoroutineExecutor executor)
        {
            executor.StartCoroutine(CalculatePowerPercentage(rigidbody));
        }

        private IEnumerator CalculatePowerPercentage(Rigidbody rigidbody)
        {
            var maxSpeed = rigidbody.velocity.magnitude;

            yield return new WaitForSmallDelay();

            while (rigidbody.velocity.magnitude > 0.0f)
            {
                maxSpeed = Math.Max(maxSpeed, rigidbody.velocity.magnitude);
                PowerPercentage = Map.MapToPercentsRange(rigidbody.velocity.magnitude, 0.0f, maxSpeed);
                yield return null;
            }
        }
    }
}
