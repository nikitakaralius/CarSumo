using System;
using System.Collections.Generic;
using System.Linq;
using BaseData.Messaging;
using Sirenix.Utilities;

namespace AI.StateMachine.Common
{
	public class AIStateMachine : ITickable
	{
		private readonly Dictionary<Type, IAIState> _states;
		
		private readonly List<ITickable> _tickables;
		private readonly List<ITransferReceiver> _receivers;

		public AIStateMachine(IEnumerable<IAIState> states)
		{
			_states = states.ToDictionary(state => state.GetType(), state => state);
			_tickables = states.OfType<ITickable>().ToList();
			_receivers = states.OfType<ITransferReceiver>().ToList();
		}

		public void Enter<TState>() where TState : IAIState
		{
			Type stateToEnter = typeof(TState);

			if (_states.TryGetValue(stateToEnter, out var instance) == false)
				throw new InvalidOperationException("Trying to enter unregistered state");
			
			instance.Enter(this);
		}

		public void Transmit(object package)
		{
			_receivers
				.SelectMany(receiver => receiver
					.GetType()
					.GetBaseTypes()
					.Where(type => type.GenericArgumentsContainsTypes(package.GetType()))
					.Select(type => (Receiver: receiver, Type: type)))
				.ForEach(pair => pair.Type
					.GetMethods()
					.First()
					.Invoke(pair.Receiver, new[] {package}));
		}

		public void Tick(float deltaTime)
		{
			_tickables.ForEach(x => x.Tick(deltaTime));
		}
	}
}