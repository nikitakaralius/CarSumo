using System;
using DataModel.Vehicles;
using UniRx;

namespace CarSumo.DataModel.Accounts
{
    public class Account : IEquatable<Account>
    {
        public IReactiveProperty<string> Name { get; }
        public IReactiveProperty<Icon> Icon { get; }
        public IReactiveProperty<IVehicleLayout> VehicleLayout { get; }

        public Account(string name, Icon icon, IVehicleLayout vehicleLayout)
        {
            Name = new ReactiveProperty<string>(name);
            Icon = new ReactiveProperty<Icon>(icon);
            VehicleLayout = new ReactiveProperty<IVehicleLayout>(vehicleLayout);
        }

        public bool Equals(Account other)
        {
            if (other is null)
            {
                return false;
            }

            return Name == other.Name;
        }
    }
}