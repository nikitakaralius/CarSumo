using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

namespace Menu.GameModes.Common.Timer
{
	public class TimerSettings : MonoBehaviour, ITimerButtonSelectHandler
	{
		private IEnumerable<TimerButton> _cachedTimerButtons;
		private TimerButton _selectedButton;

		public float SelectedTimeAmount => _selectedButton.TimeAmount;
		
		private void Start()
		{
			_cachedTimerButtons = GetComponentsInChildren<TimerButton>();

			foreach (TimerButton button in _cachedTimerButtons)
			{
				button.Initialize(this);
			}
			
			_cachedTimerButtons.First().SetSelected(true);
		}

		public void OnButtonSelected(TimerButton element)
		{
			_selectedButton = element;
			SetButtonsDeselected(_cachedTimerButtons, element);
		}

		public void OnButtonDeselected(TimerButton element)
		{
			if (element == _selectedButton)
			{
				_selectedButton.SetSelected(true);
			}
		}

		private void SetButtonsDeselected(IEnumerable<TimerButton> allButtons, TimerButton selectedButton)
		{
			allButtons
				.Where(button => button != selectedButton)
				.ForEach(button => button.SetSelected(false));
		}
	}
}