using System.Threading;
using System.Threading.Tasks;

namespace AI.StateMachine.Common
{
	public interface IAsyncState
	{
		Task DoAsync(CancellationToken token);
	}
}