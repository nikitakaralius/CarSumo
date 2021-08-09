using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Vehicles
{
    public class VehicleCard : ItemDragHandler<VehicleCard>
    {
        private const float Bias = 50.0f;

        public new void Initialize(Transform contentParent, Transform draggingParent, LayoutGroup layoutGroup)
        {
            base.Initialize(contentParent, draggingParent, layoutGroup);
        }

        protected override void OnDragUpdate(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}