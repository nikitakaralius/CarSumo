using System;

namespace Advertisement.Units.Rewarded
{
	public interface IRewardedUnit
	{
		void Show(RewardedPlacement placement, Action<int> reward);
	}
}