using Cinemachine;
using Sirenix.OdinInspector;
using Zenject;

namespace CarSumo.Cameras
{
    public class CameraInputProvider : SerializedMonoBehaviour, AxisState.IInputAxisProvider
    {
        private AxisState.IInputAxisProvider _provider;

        [Inject]
        private void Construct(AxisState.IInputAxisProvider axisProvider)
        {
            _provider = axisProvider;
        }

        public float GetAxisValue(int axis)
        {
            return _provider.GetAxisValue(axis);
        }
    }
}