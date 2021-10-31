using System;
using System.Collections.Generic;
using Zenject;

namespace Advertisement.Units.Rewarded
{
	public class IronSourceRewardedUnit : IRewardedUnit, IInitializable, IDisposable
	{
		private readonly Dictionary<RewardedPlacement, string> _placements = new Dictionary<RewardedPlacement, string>()
		{
			{RewardedPlacement.ExtraAccountSlot, "ExtraSlot_Store"}
		};

		private Action<int> _reward = null;

		public void Show(RewardedPlacement placement, Action<int> reward)
		{
			IronSource.Agent.showRewardedVideo(_placements[placement]);
			_reward = reward;
		}

		public void Initialize()
		{
			IronSourceEvents.onRewardedVideoAdRewardedEvent += Reward;
		}

		public void Dispose()
		{
			IronSourceEvents.onRewardedVideoAdRewardedEvent -= Reward;
		}

		private void Reward(IronSourcePlacement placement)
		{
			int rewardAmount = placement.getRewardAmount();
			_reward?.Invoke(rewardAmount);

			_reward = null;
		}
	}
}