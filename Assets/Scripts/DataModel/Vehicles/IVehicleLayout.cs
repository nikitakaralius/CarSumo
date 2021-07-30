using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DataModel.Vehicles
{
    public interface IVehicleLayout
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }

        void ReplaceActiveVehicle(VehicleId vehicle, int slot);
        IEnumerable<GameObject> GetActiveVehiclesPrefabs();
    }
}