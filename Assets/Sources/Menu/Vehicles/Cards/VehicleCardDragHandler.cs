using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Vehicles.Cards
{
	[RequireComponent(typeof(Button))]
	public class VehicleCardDragHandler : ItemDragHandler<VehicleCardDragHandler>
	{
		private Button _button;
		private Action _onBeforeBeginDrag;
		
		public void Initialize(float requiredHoldTime,
							Action onBeforeBeginDrag,
							Transform contentParent,
							Transform draggingParent,
							LayoutGroup layoutGroup)
		{
			base.Initialize(contentParent, draggingParent, layoutGroup);
			SetRequiredHoldTime(requiredHoldTime);
			_onBeforeBeginDrag = onBeforeBeginDrag;
		}
		
		protected override void OnDragUpdate(PointerEventData eventData)
		{
			transform.position = new Vector3(eventData.position.x, transform.position.y);
		}

		protected override void OnBeforeBeginDrag()
		{
			_onBeforeBeginDrag?.Invoke();
		}

		protected override void OnAfterBeginDrag()
		{
			_button.enabled = false;
		}

		protected override void OnAfterEndDrag()
		{
			_button.enabled = true;
		}

		private void Start()
		{
			_button = GetComponent<Button>();
		}
	}
}