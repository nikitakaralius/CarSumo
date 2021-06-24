﻿using CarSumo.Teams;
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
            get => GetVehicle(team);
            set => _vehicles[(int)team] = value;
        }

        public IVehicle GetVehicle(Team team)
        {
            int index = (int)team;

            //it doesn't work and I've no idea why (condition is true in debug mode, but is skips anyway)

            //if (_vehicles[index] == null)
            //    return new IVehicle.FakeVehicle(team);

            //but this works perfectly
            if (_vehicles[index].ToString() == "null")
                return new IVehicle.FakeVehicle(team);

            return _vehicles[index];
        }
    }
}
