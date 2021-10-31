using UnityEngine;

namespace CarSumo.StateMachine.States
{
	public class WinState : IState
	{
		private float _enteredTimeScale;
        
		public void Enter()
		{
			_enteredTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}

		public void Exit()
		{
			Time.timeScale = _enteredTimeScale;
		}
	}
}