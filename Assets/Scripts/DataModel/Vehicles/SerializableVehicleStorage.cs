using System.Collections.Generic;

namespace DataModel.Vehicles
{
    public class SerializableVehicleStorage
    {
        public IEnumerable<VehicleId> Vehicles { get; set; }
    }
}