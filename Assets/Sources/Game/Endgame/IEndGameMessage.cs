using System;
using CarSumo.DataModel.Accounts;

namespace Game.Endgame
 {
	public interface IEndGameMessage
	{
		IObservable<Account> ObserveEnding();
	}
}