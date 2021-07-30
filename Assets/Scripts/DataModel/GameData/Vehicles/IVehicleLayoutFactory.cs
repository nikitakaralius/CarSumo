using System.Collections.Generic;
using DataModel.Vehicles;
using Zenject;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleLayoutFactory : IFactory<IVehicleLayout>
    {
        IVehicleLayout Create(IEnumerable<VehicleId> vehicles);
    }
}