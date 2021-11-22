using DataModel.Vehicles;
using Menu.Buttons;
using UnityEngine;

namespace Menu.Vehicles.Cards
{
    public class VehicleCard : SelectableButton<VehicleCard>
    {
        [SerializeField] private VehicleId _vehicleId;

        public VehicleId VehicleId => _vehicleId;
        public int DynamicSiblingIndex { get; private set; }

        public void Initialize(IVehicleCardSelectHandler selectHandler)
        {
	        // Initialize(this, selectHandler, false);
	        UpdateSiblingIndex();
        }

        public void SetLatestSiblingIndex()
        {
            transform.SetSiblingIndex(DynamicSiblingIndex);
        }

        public void UpdateSiblingIndex()
        {
	        DynamicSiblingIndex = transform.GetSiblingIndex();
        }

        protected override void OnButtonSelectedInternal()
        {
	        UpdateSiblingIndex();
        }

        protected override void OnButtonDeselectedInternal()
        {
	        
        }
    }
}