using System.Collections.Generic;
using DataManagement.Resources;
using DataManagement.Vehicles.Models;

namespace DataManagement.Players.Models
{
    [System.Serializable]
    public class Player
    {
        public string Name;
        public object Icon;
        
        public Dictionary<ResourceId, int> Resources;

        public Vehicle[] BoughtVehicles;
        public Vehicle[] Layout;
    }
}