using System.Collections.Generic;

namespace AI.StateMachine.Common
{
	public class AIStateMachine
	{
		private readonly IEnumerable<IAsyncState> _states;

		public AIStateMachine(IEnumerable<IAsyncState> states)
		{
			_states = states;
		}

		public async void Run()
		{
			foreach (IAsyncState state in _states) 
				await state.Do();
		}
	}
}