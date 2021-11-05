namespace AI.StateMachine.Common
{
	public interface IAIState
	{
		void Enter(AIStateMachine stateMachine);

		public class None : IAIState
		{
			public void Enter(AIStateMachine stateMachine) { }
		}
	}
}