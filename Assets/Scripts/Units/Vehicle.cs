using System;
using System.Collections;
using CarSumo.Teams;
using UnityEngine;
using CarSumo.Units.Stats;
using CarSumo.VFX;

namespace CarSumo.Units
{
    public class Vehicle : MonoBehaviour, ITeamChangeSender, IVehicleStatsProvider
    {
        public event Action ChangePerformed;

        public event Action Destroying;
        public event Action<Vehicle> Upgrading;

        private event Action Pushed;
        private event Action Stopped;

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
            Pushed += EmitPushSmokeParticles;
            Stopped += StopPushSmokeParticles;
        }

        private void OnDisable()
        {
            Pushed -= StartWaitingForZeroSpeed;
            Pushed -= EmitPushSmokeParticles;
            Stopped -= StopPushSmokeParticles;
        }


        public void Init(Team team)
        {
            _statsProvider = _typeStats.Init();
            _statsProvider = new VehicleTeamStats(_statsProvider, team);

            GetComponent<MeshRenderer>().material = _typeStats.GetMaterialByTeam(team);
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

        public void SendUpgradeRequest()
        {
            Upgrading?.Invoke(this);
        }

        public void Destroy(bool destroyWithUnit = true)
        {
            ChangePerformed?.Invoke();

            if (destroyWithUnit)
                Destroying?.Invoke();
            
            GameObject.Destroy(gameObject);
        }

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

        private IEnumerator WaitForZeroSpeed()
        {
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude > 0.0f);
            Stopped?.Invoke();
            ChangePerformed?.Invoke();
        }

        private void StartWaitingForZeroSpeed() => StartCoroutine(WaitForZeroSpeed());
        private void EmitPushSmokeParticles() => _pushSmokeParticles.Emit();
        private void StopPushSmokeParticles() => _pushSmokeParticles.Stop();
    }
}