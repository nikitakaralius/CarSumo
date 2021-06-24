using UnityEngine;
using CarSumo.Teams;
using CarSumo.Input;
using CarSumo.Extensions;

namespace CarSumo.Vehicles.Selector
{
    public class VehiclePicker
    {
        private readonly Camera _camera;
        private readonly ITeamChangeHandler _changeHandler;

        public VehiclePicker(Camera camera, ITeamChangeHandler changeHandler)
        {
            _camera = camera;
            _changeHandler = changeHandler;
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
