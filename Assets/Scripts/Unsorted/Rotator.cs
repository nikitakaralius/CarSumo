using System;
using UnityEngine;

namespace Assets.Scripts.Unsorted
{
    public class Rotator : MonoBehaviour
    {
        private enum Axis
        {
            X, Y, Z
        }

        [SerializeField] private Axis _rotateAxis;
        [SerializeField] private float _rotationSpeed;

        private void Update()
        {
            transform.Rotate(GetRotateAxisVector(_rotateAxis), _rotationSpeed * Time.deltaTime);
        }

        private Vector3 GetRotateAxisVector(Axis axis)
        {
            return axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.down,
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }
    }
}