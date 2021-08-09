using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Vehicles
{
    public class VehicleCard : MonoBehaviour
    {
        [SerializeField] private VehicleId _id;

        public VehicleId Id => _id;
    }
}