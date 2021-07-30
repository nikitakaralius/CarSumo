using CarSumo.DataModel.GameResources;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;

namespace CarSumo.DataModel.Players
{
    public class Player
    {
        public IResourceStorage ResourceStorage { get; }
        
        public IVehicleStorage VehicleStorage { get; }
        public IVehicleLayout VehicleLayout { get; }
        
        public IReactiveProperty<string> Name { get; }
        public IReactiveProperty<Sprite> Icon { get; }
        

        public Player(string name,
                           Sprite icon,
                           IResourceStorage resourceStorage,
                           IVehicleStorage vehicleStorage,
                           IVehicleLayout vehicleLayout)
        {
            Name = new ReactiveProperty<string>(name);
            Icon = new ReactiveProperty<Sprite>(icon);
            ResourceStorage = resourceStorage;
            VehicleStorage = vehicleStorage;
            VehicleLayout = vehicleLayout;
        }
    }
}