using System.Collections.Generic;

namespace DataModel.Vehicles
{
    public class SerializableVehicleStorage
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}