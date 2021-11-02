using AI.StateMachine.Common;
using AI.Structures;
using UnityEngine;

namespace AI.StateMachine.States
{
	public class AITestState : IAIState, ITransferReceiver<VehiclePair>
	{
		public void Enter(AIStateMachine stateMachine)
		{
			Debug.Log("Entered");
		}

		public void Accept(VehiclePair package)
		{
			Debug.Log(package.Controlled);
		}
	}
}