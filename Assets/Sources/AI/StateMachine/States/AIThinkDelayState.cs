using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;

namespace AI.StateMachine.States
{
	public class AIThinkDelayState : IAsyncState
	{
		private readonly int _millisecondsDelay;

		public AIThinkDelayState(int millisecondsDelay)
		{
			_millisecondsDelay = millisecondsDelay;
		}

		public async Task DoAsync(CancellationToken token)
		{
			await Task.Delay(_millisecondsDelay, token);
		}
	}
}