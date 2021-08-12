using System.Collections.Generic;
using System.Linq;
using GameModes;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.Common.Timer
{
	public class TimerSettings : MonoBehaviour, ITimerButtonSelectHandler
	{
		private IGameModeOperations _gameModeOperations;
		private IEnumerable<TimerButton> _cachedTimerButtons;
		private TimerButton _selectedButton;


		[Inject]
		private void Construct(IGameModeOperations gameModeOperations)
		{
			_gameModeOperations = gameModeOperations;
		}
		
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
			_gameModeOperations.ConfigureTimer(element.TimeAmount);
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