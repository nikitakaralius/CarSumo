using CarSumo.Teams;
using System;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleCollection
    {
        private static int s_teamCount = Enum.GetValues(typeof(Team)).Length;

        private readonly IVehicle[] _vehicles = new IVehicle[s_teamCount];

        public VehicleCollection()
        {
            for (int i = 0; i < _vehicles.Length; i++)
            {
                var team = (Team)i;
                _vehicles[i] = new IVehicle.FakeVehicle(team);
            }
        }

        public IVehicle this[Team team]
        {
            get => _vehicles[(int)team];
            set => _vehicles[(int)team] = value;
        }
    }
}
