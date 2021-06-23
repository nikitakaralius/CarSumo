using UnityEngine;
using CarSumo.NewVehicles.Stats;
using CarSumo.Teams;

namespace CarSumo.NewVehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleEngine))]
    public class Vehicle : MonoBehaviour
    {
        public VehicleEngine Engine { get; private set; }

        [SerializeField] private VehicleTypeStats _typeStats;

        private Rigidbody _rigidbody;
        private IVehicleStatsProvider _statsProvider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Engine = GetComponent<VehicleEngine>();
        }

        public void Init(Team team)
        {
            _statsProvider = _typeStats;
            _statsProvider = new VehicleTeamStats(_statsProvider, team);

            GetComponent<MeshRenderer>().material = _typeStats.GetMatetialByTeam(team);
        }
    }
}
