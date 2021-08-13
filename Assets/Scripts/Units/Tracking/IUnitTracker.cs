using CarSumo.Teams;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public interface IUnitTracker
	{
		IReadOnlyReactiveProperty<int> GetUnitsAlive(Team team);
	}
}