using System;
using System.Collections;
using CarSumo.Data;
using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class Unit : MonoBehaviour, ITeamChangeSender
    {
        public event Action ChangeSent;

        [SerializeField] private UnitData _data;

        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Push(float forceMultiplier)
        {
            var force = -transform.forward * _data.PushForce * forceMultiplier;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            StartCoroutine(WaitForZeroSpeedRoutine());
        }

        public void Rotate(Vector3 direction)
        {
            transform.forward = Vector3.MoveTowards(transform.forward,
                direction, Time.deltaTime * _data.RotationSpeed);
        }

        private IEnumerator WaitForZeroSpeedRoutine()
        {
            while (_rigidbody.velocity.magnitude >= 0.0f)
                yield return null;

            ChangeSent?.Invoke();
        }
    }
}