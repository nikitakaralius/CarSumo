using System;
using System.Collections.Generic;

namespace Advertisement.Units.Interstitial
{
	public class IronSourceInterstitialUnit : IInterstitialUnit
	{
		private readonly Dictionary<InterstitialPlacement, string> _placements = new Dictionary<InterstitialPlacement, string>
		{
			{InterstitialPlacement.BattleComplete, "Battle_Complete"}
		};

		public void Show(InterstitialPlacement placement)
		{
			if (_placements.TryGetValue(placement, out var interstitial) == false)
				throw new ArgumentOutOfRangeException(nameof(placement),
					"Interstitial Unit does not contain such placement. Make sure it has registered");
			
			IronSource.Agent.showInterstitial(interstitial);
		}
	}
}