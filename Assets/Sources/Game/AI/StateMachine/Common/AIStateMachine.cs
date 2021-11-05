using System;
using System.Collections.Generic;
using System.Linq;
using AI.StateMachine.Messaging;
using Sirenix.Utilities;

namespace AI.StateMachine.Common
{
	public class AIStateMachine
	{
		private readonly Dictionary<Type, IAIState> _states;
		private readonly List<ITransferReceiver> _receivers;

		private IAIState _current = new IAIState.None();
		
		public AIStateMachine(IEnumerable<IAIState> states)
		{
			_states = states.ToDictionary(state => state.GetType(), state => state);
			_receivers = states.OfType<ITransferReceiver>().ToList();
		}

		public void Enter<TState>() where TState : IAIState
		{
			Type stateToEnter = typeof(TState);

			if (_states.TryGetValue(stateToEnter, out var instance) == false)
				throw new InvalidOperationException("Trying to enter unregistered state");

			_current = instance;
			_current.Enter(this);
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
			if (_current is ITickable tickable)
				tickable.Tick(this, deltaTime);
		}
	}
}