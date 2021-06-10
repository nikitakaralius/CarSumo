﻿using System;
using System.Collections;
using CarSumo.Teams;
using UnityEngine;
using CarSumo.Units.Stats;
using CarSumo.VFX;

namespace CarSumo.Units
{
    public class Vehicle : MonoBehaviour, ITeamChangeSender, IVehicleStatsProvider
    {
        public event Action ChangePerformed
        {
            add => Stopped += value;
            remove => Stopped -= value;
        }

        public event Action Destroying;
        public event Action Upgrading;

        private event Action Pushed;
        private event Action Stopped;

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
            Upgrading?.Invoke();
        }

        public void Destroy()
        {
            Stopped?.Invoke();
            Destroying?.Invoke();
            Destroy(gameObject);
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
        }

        private void StartWaitingForZeroSpeed() => StartCoroutine(WaitForZeroSpeed());
        private void EmitPushSmokeParticles() => _pushSmokeParticles.Emit();
        private void StopPushSmokeParticles() => _pushSmokeParticles.Stop();
    }
}