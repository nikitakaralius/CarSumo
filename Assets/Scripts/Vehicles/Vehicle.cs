using System;
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
        private VehicleEngine _engine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _engine = new VehicleEngine(_rigidbody, this, EngineWorking, Stopped);
        }

        private void OnEnable()
        {
            Pushed += _engine.StartCalculatingEnginePower;
        }

        private void OnDisable()
        {
            Pushed -= _engine.StartCalculatingEnginePower;
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

        public void TurnOnEngineWithPower(float percentage) => _engine.TurnOnEngineWithPower(percentage);

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
    }
}