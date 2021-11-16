using System;

namespace Game.Endgame
 {
	public interface IEndGameMessage
	{
		IObservable<PersonalizedEndGameStatus> ObserveEnding();
	}
}