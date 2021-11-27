using CarSumo.Input;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Cameras
{
    public class ScaledCameraRotation : SerializedMonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _fullScreenSwipeRotation = 180.0f;

        private Camera _camera;
        private CameraInput _input;
        private CinemachineOrbitalTransposer _transposer;

        private float _xAxisAtBeginning;

        [Inject]
        private void Construct(CameraInput input, Camera camera)
        {
            _input = input;
            _camera = camera;
        }

        private void OnEnable()
        {
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            _input.Events.Begun += OnSwipeBegun;
            _input.Events.Swiping += OnSwiping;
        }

        private void OnDisable()
        {
            _input.Events.Begun -= OnSwipeBegun;
            _input.Events.Swiping -= OnSwiping;
        }

        private void OnSwipeBegun(Swipe swipe)
        {
            _xAxisAtBeginning = _transposer.m_XAxis.Value;
        }

        private void OnSwiping(Swipe swipe)
        {
            Vector3 start = _camera.ScreenToViewportPoint(swipe.StartPosition);
            Vector3 end = _camera.ScreenToViewportPoint(swipe.EndPosition);

            float percent = end.x - start.x;

            _transposer.m_XAxis.Value = percent * _fullScreenSwipeRotation + _xAxisAtBeginning;
        }
    }
}