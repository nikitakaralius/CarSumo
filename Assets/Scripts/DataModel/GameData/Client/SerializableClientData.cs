using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using DataModel.Vehicles;

namespace DataModel.GameData.Client
{
    public class SerializableClientData
    {
        public SerializableAccountStorage AccountStorage { get; set; }
        
        public IReadOnlyDictionary<ResourceId, (int, int?)> Resources { get; set; }
        
        public IEnumerable<VehicleId> BoughtVehicles { get; set; }
    }
}