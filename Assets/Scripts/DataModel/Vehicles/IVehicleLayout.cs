﻿using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleLayout
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }
        void ReplaceActiveVehicle(VehicleId vehicle, int slot);
    }
}