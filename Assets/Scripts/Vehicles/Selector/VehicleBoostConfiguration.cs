using UnityEngine;
using CarSumo.Vehicles.Speedometers;
using CarSumo.Input;
using CarSumo.Extensions;
using Cinemachine.Utility;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleBoostConfiguration
    {
        private readonly Camera _camera;
        private readonly SelectorSpeedometer _speedometer;

        public VehicleBoostConfiguration(Camera camera, SelectorSpeedometer speedometer)
        {
            _camera = camera;
            _speedometer = speedometer;
        }

        public void ConfigureBoost(Vehicle vehicle, SwipeData swipeData)
        {
            var transformedDirection = GetTransformedDirection(_camera, swipeData.Direction);
            vehicle.Rotation.RotateBy(transformedDirection);
            _speedometer.CalculatePowerBySwipeData(swipeData);
        }

        private Vector3 GetTransformedDirection(Camera camera, Vector2 swipeDirection)
        {
            var direction = new Vector3
            {
                x = swipeDirection.x,
                z = swipeDirection.y
            };

            var transformedDirection = camera.GetRelativeDirection(direction)
                                                    .ProjectOntoPlane(Vector3.up)
                                                    .normalized;

            return transformedDirection;
        }
    }
}
