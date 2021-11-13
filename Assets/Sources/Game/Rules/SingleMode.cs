using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;

namespace Game.Rules
{
	public class SingleMode
	{
		public class PickerRules : VehiclePicker.IRules
		{
			private readonly Team _ai;

			public PickerRules(Team ai) => _ai = ai;

			public bool CanPick(IVehicle vehicle) => vehicle.Stats.Team != _ai;
		}
	}
}