using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Vehicles
{
    public class VehicleCard : ItemDragHandler<VehicleCard>
    {
        private const float Bias = 50.0f;
        private ScrollRect _scrollRect;

        public void Initialize(Transform originalParent, ScrollRect scrollRect = null)
        {
            base.Initialize(originalParent);
            _scrollRect = scrollRect;
        }

        public override void OnDragUpdate(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        private void Update()
        {
            if (CanDrag.Value == false)
                return;

            if (_scrollRect != null)
            {
                float originalParentVerticalPosition = ((RectTransform)LayoutRoot).anchoredPosition.y;
                float bottomVerticalPosition = ((RectTransform)_scrollRect.transform).sizeDelta.y;

                if (transform.localPosition.y + originalParentVerticalPosition > -Bias)
                {
                    MoveScrollRect(Time.deltaTime);
                }
                if (transform.localPosition.y + originalParentVerticalPosition < bottomVerticalPosition + Bias)
                {
                    MoveScrollRect(-Time.deltaTime);
                }
            }
        }

        private void MoveScrollRect(float delta)
        {
            _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition + delta);
        }
    }
}