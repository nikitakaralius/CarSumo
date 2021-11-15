namespace Game.Endgame
{
	public interface IEndGameStatus
	{
		void Accept(IEndGameStatusVisitor visitor);
	}

	public class SingleModeWin : IEndGameStatus
	{
		public void Accept(IEndGameStatusVisitor visitor) => visitor.Visit(this);
	}
	
	public class SingleModeLose : IEndGameStatus
	{
		public void Accept(IEndGameStatusVisitor visitor) => visitor.Visit(this);
	}
	
	public class OneDeviceEndGame : IEndGameStatus
	{
		public void Accept(IEndGameStatusVisitor visitor) => visitor.Visit(this);
	}
}