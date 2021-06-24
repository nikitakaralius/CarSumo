using UnityEngine;
using CarSumo.Teams;
using CarSumo.Input;
using CarSumo.Extensions;
using CarSumo.Vehicles.Speedometers;

namespace CarSumo.Vehicles.Selector
{
    public class VehiclePicker
    {
        private readonly Camera _camera;
        private readonly VehicleCollection _lastValidVehicles;
        private readonly IVehicleSpeedometer _speedometer;
        private readonly ITeamChangeHandler _changeHandler;

        public VehiclePicker(Camera camera, 
                            VehicleCollection lastValidVehicles, 
                            IVehicleSpeedometer speedometer, 
                            ITeamChangeHandler changeHandler)
        {
            _camera = camera;
            _lastValidVehicles = lastValidVehicles;
            _speedometer = speedometer;
            _changeHandler = changeHandler;
        }

        public Vehicle GetVehicleBySwipe(SwipeData swipeData)
        {
            if (TryPickVehicle(swipeData, out var vehicle))
            {
                vehicle.Engine.TurnOn(_speedometer);
                var team = vehicle.GetStats().Team;
                _lastValidVehicles[team] = vehicle;
            }

            return vehicle;
        }

        public bool IsValid(Vehicle vehicle)
        {
            return vehicle != null && CanPickVehicle(vehicle);
        }

        public bool TryPickVehicle(SwipeData swipeData, out Vehicle vehicle)
        {
            return _camera.TryGetComponentWithRaycast(swipeData.EndPosition, out vehicle)
                   && CanPickVehicle(vehicle);
        }

        private bool CanPickVehicle(Vehicle vehicle)
        {
            return vehicle.GetStats().Team == _changeHandler.Team;
        }
    }
}
