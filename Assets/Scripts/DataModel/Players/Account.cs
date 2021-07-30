using DataModel.Vehicles;
using UniRx;
using UnityEngine;

namespace CarSumo.DataModel.Accounts
{
    public class Account
    {
        public IReactiveProperty<string> Name { get; }
        public IReactiveProperty<Sprite> Icon { get; }
        public IReactiveProperty<IVehicleLayout> VehicleLayout { get; }

        public Account(string name, Sprite icon, IVehicleLayout vehicleLayout)
        {
            Name = new ReactiveProperty<string>(name);
            Icon = new ReactiveProperty<Sprite>(icon);
            VehicleLayout = new ReactiveProperty<IVehicleLayout>(vehicleLayout);
        }
    }
}