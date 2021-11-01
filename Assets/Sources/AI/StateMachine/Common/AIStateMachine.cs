using System.Collections.Generic;
using System.Threading;

namespace AI.StateMachine.Common
{
	public class AIStateMachine
	{
		private readonly IEnumerable<IAsyncState> _sequence;

		public AIStateMachine(IEnumerable<IAsyncState> sequence)
		{
			_sequence = sequence;
		}

		public async void RunAsync(CancellationToken token)
		{
			foreach (IAsyncState state in _sequence)
			{
				if (token.IsCancellationRequested)
					return;
				
				await state.DoAsync();
			}
		}
	}
}