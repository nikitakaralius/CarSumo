using UnityEngine;
using CarSumo.NewVehicles.Stats;
using CarSumo.Teams;

namespace CarSumo.NewVehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleEngine))]
    public class Vehicle : MonoBehaviour
    {
        public IVehicleEngine Engine { get; private set; }

        [SerializeField] private VehicleTypeStats _typeStats;

        private Rigidbody _rigidbody;
        private IVehicleStatsProvider _statsProvider;

        private void Awake()
        {
            var coroutineExecutor = new CoroutineExecutor(this);

            _rigidbody = GetComponent<Rigidbody>();
            Engine = GetComponent<VehicleEngine>().Init(_statsProvider, _rigidbody, coroutineExecutor);
        }

        public void Init(Team team)
        {
            _statsProvider = _typeStats;
            _statsProvider = new VehicleTeamStats(_statsProvider, team);

            GetComponent<MeshRenderer>().material = _typeStats.GetMatetialByTeam(team);
        }
    }
}
