using CarSumo.Extensions;
using CarSumo.Input;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles.Speedometers;
using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    public class VehiclePicker
    {
        private readonly Camera _camera;
        private readonly VehicleCollection _lastValidVehicles;
        private readonly IVehicleSpeedometer _speedometer;
        private readonly ITeamPresenter _teamPresenter;

        public VehiclePicker(Camera camera,
                             VehicleCollection lastValidVehicles,
                             IVehicleSpeedometer speedometer,
                             ITeamPresenter teamPresenter)
        {
            _camera = camera;
            _lastValidVehicles = lastValidVehicles;
            _speedometer = speedometer;
            _teamPresenter = teamPresenter;
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

        private bool TryPickVehicle(SwipeData swipeData, out Vehicle vehicle)
        {
            return _camera.TryGetComponentWithRaycast(swipeData.EndPosition, out vehicle)
                   && CanPickVehicle(vehicle);
        }

        private bool CanPickVehicle(Vehicle vehicle)
        {
            return vehicle.GetStats().Team == _teamPresenter.ActiveTeam.Value;
        }
    }
}
