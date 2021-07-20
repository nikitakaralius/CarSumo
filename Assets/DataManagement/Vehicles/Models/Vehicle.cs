using System.Collections.Generic;
using DataManagement.Resources.Models;

namespace DataManagement.Vehicles.Models
{
    [System.Serializable]
    public class Vehicle
    {
        public string Name;
        public Dictionary<ResourceId, int> Costs;
        public VehicleStats Stats;
    }
}