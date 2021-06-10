using System;
using System.Collections;
using CarSumo.Data;
using CarSumo.Teams;
using CarSumo.VFX;
using UnityEngine;

namespace CarSumo.Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnitOld : MonoBehaviour, ITeamChangeSender
    {
        public event Action ChangePerformed;

        public Team Team => _team;

        [SerializeField] private Team _team;
        [SerializeField] private UnitData _data;

        [Header("Particles")]
        [SerializeField] private FXBehaviour _pushSmokeParticles;

        private Rigidbody _rigidbody;
        
        private void Start() => _rigidbody = GetComponent<Rigidbody>();

        public void Push(float forceMultiplier)
        {
            var force = -transform.forward * _data.PushForce * forceMultiplier;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            _pushSmokeParticles.Emit();

            StartCoroutine(WaitForZeroSpeedRoutine());
        }

        public void Rotate(Vector3 direction)
        {
            transform.forward = Vector3.MoveTowards(transform.forward,
                direction, Time.deltaTime * _data.RotationSpeed);
        }

        public void Destroy()
        {
            Destroy(gameObject);
            ChangePerformed?.Invoke();
        }

        private IEnumerator WaitForZeroSpeedRoutine()
        {
            while (_rigidbody.velocity.magnitude > 0.0f)
                yield return null;

            _pushSmokeParticles.Stop();

            ChangePerformed?.Invoke();
        }
    }
}