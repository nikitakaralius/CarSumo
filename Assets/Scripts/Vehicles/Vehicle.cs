using System;
using System.Collections;
using CarSumo.Teams;
using UnityEngine;
using CarSumo.Vehicles.Stats;
using CarSumo.VFX;
using CarSumo.Data;

namespace CarSumo.Vehicles
{
    public class Vehicle : MonoBehaviour, ITeamChangeSender, IVehicleStatsProvider
    {
        public event Action TeamChangePerformed;

        public event Action Destroying;
        public event Action Upgrading;

        public event Action Pushed;
        public event Action Unpicked;
        public event Action Stopped;

        public event Action Picked;
        public event Action<float> StartingUp;

        public WorldPlacement WorldPlacement => new WorldPlacement(transform.position, transform.forward);

        [Header("Preferences")]
        [SerializeField] private VehicleTypeStats _typeStats;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("VFX")]
        [SerializeField] private FXBehaviour _pushSmokeParticles;

        private IVehicleStatsProvider _statsProvider;

        private void OnEnable()
        {
            Pushed += StartWaitingForZeroSpeed;
            Pushed += StartCalculatingEnginePower;
            Picked += EmitPushSmokeParticles;
            Unpicked += StopPushSmokeParticles;
            Stopped += StopPushSmokeParticles;
        }

        private void OnDisable()
        {
            Pushed -= StartWaitingForZeroSpeed;
            Pushed -= StartCalculatingEnginePower;
            Picked -= EmitPushSmokeParticles;
            Unpicked -= StopPushSmokeParticles;
            Stopped -= StopPushSmokeParticles;
        }

        public void Init(Team team)
        {
            _statsProvider = _typeStats.Init();
            _statsProvider = new VehicleTeamStats(_statsProvider, team);

            GetComponent<MeshRenderer>().material = _typeStats.GetMaterialByTeam(team);
        }

        public void Pick()
        {
            Picked?.Invoke();
        }

        public void Unpick()
        {
            Unpicked?.Invoke();
        }

        public void PushForward(float extraForceModifier)
        {
            var force = transform.forward * GetStats().PushForceModifier * extraForceModifier;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            Pushed?.Invoke();
        }

        public void RotateByForwardVector(Vector3 forwardVector)
        {
            var speed = GetStats().RotationSpeed * Time.deltaTime;
            transform.forward = Vector3.MoveTowards(transform.forward, forwardVector, speed);   
        }

        public void Upgrade()
        {
            Upgrading?.Invoke();
        }

        public void Destroy()
        {
            TeamChangePerformed?.Invoke();
            Destroying?.Invoke();
            
            Destroy(gameObject);
        }

        public void DestroyWithoutNotification() => Destroy(gameObject);

        public void SetWorldPlacement(WorldPlacement placement)
        {
            transform.position = placement.Position;
            transform.forward = placement.ForwardVector;
        }

        public VehicleStats GetStats()
        {
            return _statsProvider.GetStats();
        }

        public IVehicleStatsProvider GetStatsProvider() => _statsProvider;
        
        public void PassPowerPercentage(float percentage)
        {
            StartingUp?.Invoke(percentage);
        }

        private IEnumerator CalculateEnginePower()
        {
            var maxSpeed = _rigidbody.velocity.magnitude;

            yield return new WaitForSeconds(0.2f);

            while (_rigidbody.velocity.magnitude > 0.0f)
            {
                maxSpeed = Math.Max(maxSpeed, _rigidbody.velocity.magnitude);

                var percentage = Converter.MapToPercents(_rigidbody.velocity.magnitude, 0.0f, maxSpeed);
                PassPowerPercentage(percentage);
                yield return null;
            }
        }

        private IEnumerator WaitForZeroSpeed()
        {
            yield return new WaitForSeconds(0.2f);

            yield return new WaitWhile(() => _rigidbody.velocity.magnitude > 0.0f);

            Stopped?.Invoke();
            TeamChangePerformed?.Invoke();
        }

        private void StartWaitingForZeroSpeed() => StartCoroutine(WaitForZeroSpeed());
        private void StartCalculatingEnginePower() => StartCoroutine(CalculateEnginePower());
        private void EmitPushSmokeParticles() => _pushSmokeParticles.Emit();
        private void StopPushSmokeParticles() => _pushSmokeParticles.Stop();
    }
}