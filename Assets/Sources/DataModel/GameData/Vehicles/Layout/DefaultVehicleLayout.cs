using System.Collections;
using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public class DefaultVehicleLayout : IEnumerable<VehicleId>
    {
        private readonly IEnumerable<VehicleId> _layout = new VehicleId[0];
        
        public IEnumerator<VehicleId> GetEnumerator()
        {
            return _layout.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) this).GetEnumerator();
        }
    }
}