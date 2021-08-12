using DataModel.Vehicles;
using Menu.Buttons;
using UnityEngine;

namespace Menu.Vehicles.Cards
{
    public class VehicleCard : SelectableButton<VehicleCard>
    {
        [SerializeField] private VehicleId _vehicleId;

        public VehicleId VehicleId => _vehicleId;
        public int DynamicSiblingIndex { get; set; }

        public void Initialize(IVehicleCardSelectHandler selectHandler)
        {
	        Initialize(this, selectHandler, false);
	        DynamicSiblingIndex = transform.GetSiblingIndex();
        }

        public void SetLatestSiblingIndex()
        {
            transform.SetSiblingIndex(DynamicSiblingIndex);
        }

        protected override void OnButtonSelectedInternal()
        {
	        DynamicSiblingIndex = transform.GetSiblingIndex();
        }

        protected override void OnButtonDeselectedInternal()
        {
        }
    }
}