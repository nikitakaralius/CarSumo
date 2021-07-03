using System.Collections;
using CarSumo.Coroutines;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.Vehicles.Speedometers;
using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    public class SelectorMoveHandler
    {
        private readonly ITeamChangeHandler _changeHandler;
        private readonly IVehicleSpeedometer _speedometer;
        private readonly VehicleSelectorData _data;
        private readonly CoroutineExecutor _executor;

        private bool _isMovePerforming = false;

        public SelectorMoveHandler(
            ITeamChangeHandler changeHandler,
            IVehicleSpeedometer speedometer,
            VehicleSelectorData data,
            CoroutineExecutor executor)
        {
            _changeHandler = changeHandler;
            _speedometer = speedometer;
            _data = data;
            _executor = executor;
        }

        public void HadnleVehiclePush(Vehicle vehicle, SwipeData swipeData)
        {
            if (_isMovePerforming)
                return;

            if (_speedometer.PowerPercentage <= _data.CancelDistancePercent)
            {
                vehicle.Engine.TurnOff();
                return;
            }

            var forceModifier = CalculateForceMultiplier(swipeData);
            vehicle.Engine.SpeedUp(forceModifier);
            _executor.StartCoroutine(PerformMove());
        }

        public bool CanPeformMove()
        {
            return _isMovePerforming == false;
        }

        private float CalculateForceMultiplier(SwipeData swipeData)
        {
            var clampedDistance = Mathf.Clamp(swipeData.Distance, _data.MinSelectDistance, _data.MaxSelectDistance);

            var part = (clampedDistance - _data.MinSelectDistance) / (_data.MaxSelectDistance - _data.MinSelectDistance);

            return _data.MaxAccelerationMultiplier * part;
        }

        private IEnumerator PerformMove()
        {
            _isMovePerforming = true;

            yield return new WaitForSeconds(_data.TimeForMove);

            _isMovePerforming = false;
            _changeHandler.ChangeTeam();
        }
    }
}
