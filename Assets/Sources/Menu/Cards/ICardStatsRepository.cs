namespace Menu.Cards
{
	public interface ICardStatsRepository
	{
		VerboseVehicleStats StatsOf(ICard card);
	}
}