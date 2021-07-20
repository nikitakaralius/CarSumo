using System.Collections.Generic;
using DataManagement.Resources.Models;
using DataManagement.Vehicles.Models;

namespace DataManagement.Players.Models
{
    [System.Serializable]
    public class Player
    {
        public string Name;
        public string Icon;
        
        public Dictionary<ResourceId, int> Resources;

        public Vehicle[] BoughtVehicles;
        public Vehicle[] Layout;
    }
}