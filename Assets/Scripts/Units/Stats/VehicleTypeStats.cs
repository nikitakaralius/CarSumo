using System.Collections.Generic;
using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Units.Stats
{
    [CreateAssetMenu(fileName = "Vehicle Type Stats", menuName = "CarSumo/Vehicles/Type Stats")]
    public class VehicleTypeStats : VehicleStatsDecoratorScriptableObject
    {
        [SerializeField] private float _pushForceModifier = 5.0f;
        [SerializeField] private float _rotationSpeed = 25.0f;
        [SerializeField] private Dictionary<Team, Material> _materials;

        public override VehicleTypeStats Init()
        {
            return this;
        }

        protected override VehicleStats GetStatsInternal()
        {
            return new VehicleStats
            {
                PushForceModifier = _pushForceModifier,
                RotationSpeed = _rotationSpeed
            };
        }

        public Material GetMaterialByTeam(Team team)
        {
            return _materials[team];
        }
    }
}