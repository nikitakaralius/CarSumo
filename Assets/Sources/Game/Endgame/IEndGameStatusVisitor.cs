namespace Game.Endgame
{
	public interface IEndGameStatusVisitor
	{
		void Visit(SingleModeWin status);
		void Visit(SingleModeLose status);
		void Visit(OneDeviceEndGame status);
	}
}