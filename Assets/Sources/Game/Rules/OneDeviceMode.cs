using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;

namespace Game.Rules
{
	public class OneDeviceMode
	{
		public class PickerRules : VehiclePicker.IRules
		{
			private readonly ITeamPresenter _teamPresenter;

			public PickerRules(ITeamPresenter teamPresenter) => _teamPresenter = teamPresenter;

			public bool CanPick(IVehicle vehicle) => vehicle.Stats.Team == _teamPresenter.ActiveTeam.Value;
		}
	}
}