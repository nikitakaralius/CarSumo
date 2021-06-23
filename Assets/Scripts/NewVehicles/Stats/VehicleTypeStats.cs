using UnityEngine;
using System.Collections.Generic;
using CarSumo.Teams;

namespace CarSumo.NewVehicles.Stats
{
    [CreateAssetMenu(fileName = "[Vehicle] Type Stats", menuName = "CarSumo/Vehicles/Type Stats")]
    public class VehicleTypeStats : VehicleStatsDecoratorSO
    {
        [SerializeField] private float _enginePower = 5.0f;
        [SerializeField] private float _rotationalSpeed = 25.0f;
        [SerializeField] private IReadOnlyDictionary<Team, Material> _materials = new Dictionary<Team, Material>();

        public override VehicleStats GetStatsInternal()
        {
            return new VehicleStats(Team.First, _enginePower, _rotationalSpeed);
        }

        public Material GetMatetialByTeam(Team team)
        {
            return _materials[team];
        }
    }
}
