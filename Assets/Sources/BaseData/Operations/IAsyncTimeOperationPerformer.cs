using System.Threading;
using System.Threading.Tasks;

namespace Sources.BaseData.Operations
{
	public interface IAsyncTimeOperationPerformer
	{
		Task DoUntilTimeElapsedAsync(CancellationToken token, float duration, TimeOperationCallback operation);
	}
}