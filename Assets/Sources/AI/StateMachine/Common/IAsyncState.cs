using System.Threading.Tasks;

namespace AI.StateMachine.Common
{
	public interface IAsyncState
	{
		Task Do();
	}
}