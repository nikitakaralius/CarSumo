using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;

namespace Game.Rules
{
	public class SingleMode
	{
		public class PickerRules : VehiclePicker.IRules
		{
			private readonly Team _ai;
			private readonly ITeamPresenter _teamPresenter;

			public PickerRules(Team ai, ITeamPresenter teamPresenter)
			{
				_ai = ai;
				_teamPresenter = teamPresenter;
			}

			public bool CanPick(IVehicle vehicle) => vehicle.Stats.Team != _ai 
			                                         && vehicle.Stats.Team == _teamPresenter.ActiveTeam.Value;
		}
	}
}