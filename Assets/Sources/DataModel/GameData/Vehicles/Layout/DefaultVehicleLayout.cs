using System.Collections;
using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public class DefaultVehicleLayout : IEnumerable<Vehicle>
    {
        private readonly IEnumerable<Vehicle> _layout = new Vehicle[0];
        
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return _layout.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) this).GetEnumerator();
        }
    }
}