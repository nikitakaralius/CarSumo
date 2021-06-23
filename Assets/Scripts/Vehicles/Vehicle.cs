using System;
using System.Collections;
using CarSumo.Teams;
using UnityEngine;
using CarSumo.Vehicles.Stats;
using CarSumo.Data;

namespace CarSumo.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MonoBehaviour, IVehicleStatsProvider
    {
        public event Action Destroying;
        public event Action Upgrading;
        public event Action<float> EngineWorking;

        public event Action Pushed;
        public event Action Unpicked;
        public event Action Stopped;

        public event Action Picked;

        public WorldPlacement WorldPlacement => new WorldPlacement(transform.position, transform.forward);

        [Header("Preferences")]
        [SerializeField] private VehicleTypeStats _typeStats;

        private IVehicleStatsProvider _statsProvider;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Pushed += StartWaitingForZeroSpeed;
            Pushed += StartCalculatingEnginePower;
        }

        private void OnDisable()
        {
            Pushed -= StartWaitingForZeroSpeed;
            Pushed -= StartCalculatingEnginePower;
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
            Stopped?.Invoke();
            Upgrading?.Invoke();
        }

        public void Destroy()
        {
            Destroying?.Invoke();
            
            Destroy(gameObject);

            Stopped?.Invoke();
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
        
        public void TurnOnEngineWithPower(float percentage)
        {
            if (percentage < 0.0f || percentage > 100.0f)
                throw new ArgumentOutOfRangeException(nameof(percentage));

            EngineWorking?.Invoke(percentage);
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
        }

        private IEnumerator WaitForZeroSpeed()
        {
            yield return new WaitForSmallDelay();

            yield return new WaitWhile(() => _rigidbody.velocity.magnitude > 0.0f);

            Stopped?.Invoke();
        }

        private void StartWaitingForZeroSpeed() => StartCoroutine(WaitForZeroSpeed());
        private void StartCalculatingEnginePower() => StartCoroutine(CalculateEnginePower());
    }
}