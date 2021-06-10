using UnityEngine;

namespace CarSumo.Extensions
{
    public static class CameraExtensions
    {
        public static bool TryGetComponentWithRaycast<T>(this Camera camera, Vector3 screenPosition, out T component)
        {
            component = default;

            var ray = camera.ScreenPointToRay(screenPosition);

            return Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent(out component);
        }
    }
}