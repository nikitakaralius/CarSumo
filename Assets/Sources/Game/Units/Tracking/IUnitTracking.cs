using CarSumo.Teams;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public interface IUnitTracking
	{
		IReadOnlyReactiveCollection<IUnit> UnitsAlive(Team team);
	}
}