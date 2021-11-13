using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;

namespace Game.Rules
{
	public class RulesRepository
	{
		private readonly Dictionary<Type, object> _rules;
		
		public RulesRepository(ITeamPresenter teamPresenter)
		{
			_rules = RegisterRules(teamPresenter)
				.ToDictionary(x => x.GetType(), x => x);
		}

		public TRule InstanceOf<TRule>()
		{
			Type rule = typeof(TRule);

			if (_rules.TryGetValue(rule, out var instance) == false)
				throw new InvalidOperationException("Trying to use unregistered rule.\n" +
				                                    $" If it is not a bug and you are going to use it, register the rule in the {nameof(RulesRepository)}");

			return (TRule) instance;
		}

		private IEnumerable<object> RegisterRules(ITeamPresenter teamPresenter)
		{
			yield return new SingleMode.PickerRules(Team.Red, teamPresenter);
			yield return new OneDeviceMode.PickerRules(teamPresenter);
		}
	}
}