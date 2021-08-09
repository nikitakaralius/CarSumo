using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Vehicles
{
    public class VehicleCard : ItemDragHandler<VehicleCard>
    {
        public new void Initialize(Transform originalParent, Transform draggingParent)
        {
            base.Initialize(originalParent, draggingParent);
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}