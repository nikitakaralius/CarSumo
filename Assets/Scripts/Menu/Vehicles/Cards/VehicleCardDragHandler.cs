using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Vehicles.Cards
{
	[RequireComponent(typeof(Button))]
	public class VehicleCardDragHandler : ItemDragHandler<VehicleCardDragHandler>
	{
		private Button _button;
		
		public void Initialize(float requiredHoldTime, Transform contentParent, Transform draggingParent, LayoutGroup layoutGroup)
		{
			base.Initialize(contentParent, draggingParent, layoutGroup);
			SetRequiredHoldTime(requiredHoldTime);
		}
		
		protected override void OnDragUpdate(PointerEventData eventData)
		{
			transform.position = new Vector3(eventData.position.x, transform.position.y);
		}

		protected override void OnLateBeginDrag()
		{
			_button.enabled = false;
		}

		protected override void OnLateEndDrag()
		{
			_button.enabled = true;
		}

		private void Start()
		{
			_button = GetComponent<Button>();
		}
	}
}