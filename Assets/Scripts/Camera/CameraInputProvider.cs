using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Cameras
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