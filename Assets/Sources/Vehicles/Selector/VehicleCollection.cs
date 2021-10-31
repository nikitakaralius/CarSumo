using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleCollection : IEnumerable<IVehicle>
    {
        private static readonly int s_teamCount = Enum.GetValues(typeof(Team)).Length;

        private readonly IVehicle[] _vehicles = new IVehicle[s_teamCount];
        
        public int Count => _vehicles.Length;

        public IVehicle this[Team team]
        {
            get => GetVehicle(team);
            set => AddVehicle(value, team);
        }

        public IVehicle GetVehicle(Team team)
        {
            int index = (int)team;

            return _vehicles[index];
        }

        public void AddVehicle(IVehicle vehicle)
        {
            AddVehicle(vehicle, vehicle.Stats.Team);
        }

        public void AddVehicle(IVehicle vehicle, Team team)
        {
            if (vehicle is null)
                throw new NullReferenceException();

            if (vehicle.Stats.Team != team)
                throw new InvalidOperationException(nameof(team));

            _vehicles[(int)team] = vehicle;
        }

        public IEnumerator<IVehicle> GetEnumerator()
        {
            return ((IEnumerable<IVehicle>) _vehicles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool IsDestroyed(IVehicle vehicle)
        {
            // this doesn't work, probably because Unity is destroying it but doesn't set the object to null
            // (condition is true in debug mode but is skips anyway)

            // return vehicle is null;
            
            return vehicle.ToString() == "null";
        }
    }
}