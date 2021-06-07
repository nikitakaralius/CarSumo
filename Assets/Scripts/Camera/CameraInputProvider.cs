using System;
using Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.Camera
{
    public class CameraInputProvider : SerializedMonoBehaviour, AxisState.IInputAxisProvider
    {
        [SerializeField] private AxisState.IInputAxisProvider _provider;

        public float GetAxisValue(int axis)
        {
            return _provider.GetAxisValue(axis);
        }
    }
}