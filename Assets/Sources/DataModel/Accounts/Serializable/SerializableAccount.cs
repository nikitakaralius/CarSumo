using System;
using System.Collections.Generic;
using DataModel.Vehicles;

namespace CarSumo.DataModel.Accounts
{
    public class SerializableAccount
    {
        public string Name { get; set; }
        
        public string Icon { get; set; }

        public IEnumerable<VehicleId> VehicleLayout { get; set; }
    }
    
    public class UnregisteredSerializableAccount : SerializableAccount
    {
        public UnregisteredSerializableAccount()
        {
            Name = "Unregistered";
            Icon = null;
            VehicleLayout = Array.Empty<VehicleId>();
        }
    }
}