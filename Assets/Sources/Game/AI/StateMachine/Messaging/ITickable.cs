using AI.StateMachine.Common;

namespace AI.StateMachine.Messaging
{
	public interface ITickable
	{
		void Tick(AIStateMachine stateMachine, float deltaTime);
	}
}