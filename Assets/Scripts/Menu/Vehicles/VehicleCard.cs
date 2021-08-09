using System;
using TweenAnimations;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Vehicles
{
    public class VehicleCard : ItemDragHandler<VehicleCard>
    {
        public new void Initialize(Transform originalParent)
        {
            base.Initialize(originalParent);
        }

        public override void OnDragUpdate(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }
    }
}