using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Buttons
{
	[RequireComponent(typeof(Button))]
	public abstract class SelectableButton<THandlerElement> : SerializedMonoBehaviour
	{
		private readonly ReactiveProperty<bool> _selected = new ReactiveProperty<bool>(false);
		private bool _notificationEnabled = true;

		private IDisposable _selectedSubscription;
		private IButtonSelectHandler<THandlerElement> _selectHandler;
		
		public Button Button { get; private set; }
		
		protected void Initialize(THandlerElement element, IButtonSelectHandler<THandlerElement> selectHandler, bool notifyOnInitialize = true)
		{
			Button = GetComponent<Button>();
			Button.onClick.AddListener(() => _selected.Value = !_selected.Value);
			
			_selectHandler = selectHandler;

			_notificationEnabled = notifyOnInitialize;

			_selectedSubscription = _selected.Subscribe(selected =>
			{
				if (_notificationEnabled == false)
				{
					return;
				}

				if (selected)
				{
					OnButtonSelectedInternal();
					_selectHandler?.OnButtonSelected(element);
				}
				else
				{
					OnButtonDeselectedInternal();
					_selectHandler?.OnButtonDeselected(element);	
				}
			});
			
			_notificationEnabled = true;
		}

		public void SetSelected(bool selected)
		{
			_selected.Value = selected;
		}

		private void OnDestroy()
		{
			_selectedSubscription?.Dispose();
		}

		protected abstract void OnButtonSelectedInternal();

		protected abstract void OnButtonDeselectedInternal();
	}
}