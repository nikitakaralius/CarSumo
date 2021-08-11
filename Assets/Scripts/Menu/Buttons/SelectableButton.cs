using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Buttons
{
	[RequireComponent(typeof(Button))]
	public abstract class SelectableButton<THandler, THandlerParameter> : SerializedMonoBehaviour
		where THandler : IButtonSelectHandler<THandlerParameter>
	{
		private readonly ReactiveProperty<bool> _selected = new ReactiveProperty<bool>(false);
		private bool _notificationEnabled = true;

		private IDisposable _selectedSubscription;
		private THandler _selectHandler;
		
		protected void Initialize(THandlerParameter parameter, [CanBeNull] THandler selectHandler, bool notifyOnInitialize = true)
		{
			Button button = GetComponent<Button>();
			button.onClick.AddListener(() => _selected.Value = !_selected.Value);
			
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
					_selectHandler?.OnButtonSelected(parameter);
				}
				else
				{
					OnButtonDeselectedInternal();
					_selectHandler?.OnButtonDeselected(parameter);	
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
			_selectedSubscription.Dispose();
		}

		protected abstract void OnButtonSelectedInternal();

		protected abstract void OnButtonDeselectedInternal();
	}
}