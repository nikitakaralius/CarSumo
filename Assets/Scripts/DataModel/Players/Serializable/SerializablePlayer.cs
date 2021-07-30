using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using DataModel.Vehicles;

namespace CarSumo.DataModel.Players
{
    public class SerializablePlayer
    {
        public string Name { get; set; }
        
        public string Icon { get; set; }
        
        public IReadOnlyDictionary<ResourceId, (int, int?)> ResourceStorage { get; set; }

        public IEnumerable<VehicleId> BoughtVehicles { get; set; }
        
        public IEnumerable<VehicleId> VehicleLayout { get; set; }
    }
}