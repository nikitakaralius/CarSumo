using CarSumo.Teams;
using CarSumo.Vehicles.Stats;
using UnityEngine;

namespace CarSumo.Vehicles
{
    public interface IVehicle : IVehicleStatsProvider
    {
        void Destroy();

        public class FakeVehicle : IVehicle
        {
            private readonly Team _team;
            
            public FakeVehicle(Team team) => _team = team;

            public void Destroy() { }

            public VehicleStats GetStats() => new VehicleStats(_team, 0.0f, 0.0f);

            public void Upgrade() { }
        }

        public class FakeVehicleMono : MonoBehaviour, IVehicle
        {
            private Team _team;

            public FakeVehicleMono Init(Team team)
            {
                _team = team;
                return this;
            }

            public void Destroy() => Destroy(gameObject);

            public VehicleStats GetStats() => new VehicleStats(_team, 0.0f, 0.0f);

            //Tests will pass wrong without Debug Log Statement
            public void Upgrade() => Debug.Log(gameObject);
        }
    }
}
