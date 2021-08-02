using DataModel.Vehicles;
using UniRx;

namespace CarSumo.DataModel.Accounts
{
    public class Account
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
    }
}