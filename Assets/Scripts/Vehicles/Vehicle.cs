using UnityEngine;
using CarSumo.Vehicles.Stats;
using CarSumo.Teams;
using CarSumo.Data;
using CarSumo.Vehicles.Rotation;
using CarSumo.Vehicles.Engine;

namespace CarSumo.Vehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleEngine), typeof(VehicleCollision))]
    public class Vehicle : MonoBehaviour, IVehicle
    {
        public IVehicleEngine Engine { get; private set; }
        public IRotation Rotation { get; private set;  }

        [SerializeField] private VehicleTypeStats _typeStats;

        private Rigidbody _rigidbody;
        private IVehicleStatsProvider _statsProvider;

        private IVehicleUpgrader _upgrader;
        private IVehicleDestroyer _destroyer;

        private bool _initialized;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public Vehicle Init(Team team, WorldPlacement placement, IVehicleUpgrader upgrader, IVehicleDestroyer destroyer)
        {
            _statsProvider = _typeStats;
            _statsProvider = new VehicleTeamStats(_statsProvider, team);
            
            var coroutineExecutor = new CoroutineExecutor(this);
            Engine = GetComponent<VehicleEngine>().Init(_statsProvider, _rigidbody, coroutineExecutor);
            Rotation = new ForwardVectorVehicleRotation(transform, _statsProvider);
            
            GetComponent<MeshRenderer>().material = _typeStats.GetMatetialByTeam(team);

            transform.position = placement.Position;
            transform.forward = placement.ForwardVector;

            _upgrader = upgrader;
            _destroyer = destroyer;

            return this;
        }

        public VehicleStats GetStats()
        {
            return _statsProvider.GetStats();
        }

        public void Destroy() => _destroyer.Destroy(this);

        public void Upgrade() => _upgrader.Upgrade(this);
    }
}
