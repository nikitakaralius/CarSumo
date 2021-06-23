using System;
using System.Collections;
using UnityEngine;

namespace CarSumo.Vehicles
{
    public class VehicleEngine
    {
        private readonly Rigidbody _rigidbody;
        private readonly Vehicle _executor;

        private event Action Stopped;
        private event Action<float> EngineWorking;

        public VehicleEngine(Rigidbody rigidbody, Vehicle executor, Action<float> engineWorking, Action stopped)
        {
            _rigidbody = rigidbody;
            _executor = executor;
            EngineWorking = engineWorking;
            Stopped = stopped;
        }

        public void TurnOnEngineWithPower(float percentage)
        {
            if (percentage < 0.0f || percentage > 100.0f)
                throw new ArgumentOutOfRangeException(nameof(percentage));

            EngineWorking.Invoke(percentage);
        }

        public void StartCalculatingEnginePower()
        {
            _executor.StartCoroutine(CalculateEnginePower());
        }

        private IEnumerator CalculateEnginePower()
        {
            var maxSpeed = _rigidbody.velocity.magnitude;

            yield return new WaitForSmallDelay();

            while (_rigidbody.velocity.magnitude > 0.0f)
            {
                maxSpeed = Math.Max(maxSpeed, _rigidbody.velocity.magnitude);
                var percentage = Converter.MapToPercents(_rigidbody.velocity.magnitude, 0.0f, maxSpeed);

                TurnOnEngineWithPower(percentage);
                yield return null;
            }

            Debug.Log("Stopped");
            Stopped.Invoke();
        }
    }
}