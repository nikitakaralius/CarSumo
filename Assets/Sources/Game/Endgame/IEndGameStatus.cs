using System;
using CarSumo.DataModel.Accounts;

namespace Game.Endgame
{
	public abstract class PersonalizedEndGameStatus
	{
		public readonly Account Winner;

		protected PersonalizedEndGameStatus(Account winner) => Winner = winner;

		public void Match(
			Action<SingleModeWin> singleModeWinMatch,
			Action<SingleModeLose> singleModeLoseMatch,
			Action<OneDeviceEndGame> oneDeviceEndGameMatch)
		{
			
			switch (this)
			{
				case SingleModeWin win:
					singleModeWinMatch.Invoke(win);
					break;
				case SingleModeLose lose:
					singleModeLoseMatch.Invoke(lose);
					break;
				case OneDeviceEndGame oneDevice:
					oneDeviceEndGameMatch.Invoke(oneDevice);
					break;
			}
		}
	}

	public class SingleModeWin : PersonalizedEndGameStatus
	{
		public SingleModeWin(Account winner) : base(winner) { }
	}
	
	public class SingleModeLose : PersonalizedEndGameStatus
	{
		public SingleModeLose(Account winner) : base(winner) { }
	}
	
	public class OneDeviceEndGame : PersonalizedEndGameStatus
	{
		public OneDeviceEndGame(Account winner) : base(winner) { }
	}
}