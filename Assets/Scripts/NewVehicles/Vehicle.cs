using UnityEngine;
using CarSumo.NewVehicles.Stats;
using CarSumo.Teams;
using CarSumo.Data;
using CarSumo.NewVehicles.Rotation;

namespace CarSumo.NewVehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleEngine), typeof(VehicleCollision))]
    public class Vehicle : MonoBehaviour
    {
        public IVehicleEngine Engine { get; private set; }
        public IRotation Rotation { get; private set;  }

        [SerializeField] private VehicleTypeStats _typeStats;

        private Rigidbody _rigidbody;
        private IVehicleStatsProvider _statsProvider;

        private IVehicleUpgrader _upgrader;
        private IVehicleDestroyer _destroyer;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Team team, WorldPlacement placement, IVehicleUpgrader upgrader, IVehicleDestroyer destroyer)
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
        }

        public void Destroy() => _destroyer.Destroy(this);

        public void Upgrade() => _upgrader.Upgrade(this);
    }
}
