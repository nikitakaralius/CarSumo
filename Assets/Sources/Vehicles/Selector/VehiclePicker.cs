using CarSumo.Extensions;
using CarSumo.Input;
using CarSumo.Vehicles.Speedometers;
using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    public class VehiclePicker
    {
        public interface IRules { bool CanPick(IVehicle vehicle); }

        private readonly Camera _camera;
        private readonly VehicleCollection _lastValidVehicles;
        private readonly IVehicleSpeedometer _speedometer;
        private readonly IRules _rules;

        public VehiclePicker(Camera camera,
                             VehicleCollection lastValidVehicles,
                             IVehicleSpeedometer speedometer,
                             IRules rules)
        {
            _camera = camera;
            _lastValidVehicles = lastValidVehicles;
            _speedometer = speedometer;
            _rules = rules;
        }

        public Vehicle GetVehicleBySwipe(SwipeData swipeData)
        {
            if (TryPickVehicle(swipeData, out var vehicle))
            {
                vehicle.Engine.TurnOn(_speedometer);
                var team = vehicle.Stats.Team;
                _lastValidVehicles[team] = vehicle;
            }

            return vehicle;
        }

        public bool IsValid(Vehicle vehicle)
        {
            return vehicle != null && CanPickVehicle(vehicle);
        }

        private bool TryPickVehicle(SwipeData swipeData, out Vehicle vehicle)
        {
            return _camera.TryGetComponentWithRaycast(swipeData.EndPosition, out vehicle)
                   && CanPickVehicle(vehicle);
        }

        private bool CanPickVehicle(IVehicle vehicle)
        {
            return _rules.CanPick(vehicle);
        }
    }
}
