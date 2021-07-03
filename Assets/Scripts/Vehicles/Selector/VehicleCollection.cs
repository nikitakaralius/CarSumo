﻿using System;
using System.Collections;
using System.Collections.Generic;
using CarSumo.Teams;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleCollection : IEnumerable<IVehicle>
    {
        private static readonly int s_teamCount = Enum.GetValues(typeof(Team)).Length;

        private readonly IVehicle[] _vehicles = new IVehicle[s_teamCount];

        public VehicleCollection()
        {
            for (int i = 0; i < _vehicles.Length; i++)
            {
                var team = (Team)i;
                _vehicles[i] = new IVehicle.FakeVehicle(team);
            }
        }

        public int Count => _vehicles.Length;

        public IVehicle this[Team team]
        {
            get => GetVehicle(team);
            set => AddVehicle(value, team);
        }

        public IVehicle GetVehicle(Team team)
        {
            int index = (int)team;

            if (IsDestroyed(_vehicles[index]))
                return new IVehicle.FakeVehicle(team);

            return _vehicles[index];
        }

        public void AddVehicle(IVehicle vehicle)
        {
            AddVehicle(vehicle, vehicle.GetStats().Team);
        }

        public void AddVehicle(IVehicle vehicle, Team team)
        {
            if (vehicle is null)
                throw new NullReferenceException();

            if (vehicle.GetStats().Team != team)
                throw new InvalidOperationException(nameof(team));

            _vehicles[(int)team] = vehicle;
        }

        public IEnumerator<IVehicle> GetEnumerator()
        {
            foreach (IVehicle vehicle in _vehicles)
                yield return vehicle;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool IsDestroyed(IVehicle vehilce)
        {
            // this doesn't work, probably because Unity is destroying it but doesn't set the object to null
            // (condition is true in debug mode but is skips anyway)

            // return vehilce is null;

            return vehilce.ToString() == "null";
        }
    }
}