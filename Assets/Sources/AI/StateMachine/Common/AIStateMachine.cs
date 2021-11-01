using System.Collections.Generic;

namespace AI.StateMachine.Common
{
	public class AIStateMachine
	{
		private readonly IEnumerable<IAsyncState> _sequence;

		public AIStateMachine(IEnumerable<IAsyncState> sequence)
		{
			_sequence = sequence;
		}

		public async void RunAsync()
		{
			foreach (IAsyncState state in _sequence) 
				await state.DoAsync();
		}
	}
}