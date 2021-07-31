﻿using System.Collections.Generic;
using DataModel.Vehicles;

namespace CarSumo.DataModel.Accounts
{
    public class SerializableAccount
    {
        public string Name { get; set; }
        
        public string Icon { get; set; }

        public IEnumerable<VehicleId> VehicleLayout { get; set; }
    }
}