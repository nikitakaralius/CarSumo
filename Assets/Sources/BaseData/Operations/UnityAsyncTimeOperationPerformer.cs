using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Sources.BaseData.Operations
{
	public class UnityAsyncTimeOperationPerformer : IAsyncTimeOperationPerformer
	{
		public async Task DoUntilTimeElapsedAsync(CancellationToken token, float duration, TimeOperationCallback operation)
		{
			float enteredTime = Time.time;

			while (token.IsCancellationRequested == false && Time.time <= duration + enteredTime)
			{
				float timePercentElapsed = (Time.time - enteredTime) / duration;
				operation.Invoke(timePercentElapsed);
				await Task.Yield();
			}
		}
	}
}