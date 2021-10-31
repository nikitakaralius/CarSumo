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
			if (_placements.TryGetValue(placement, out var rewarded) == false)
				throw new ArgumentOutOfRangeException(nameof(placement),
					"Rewarded Unit does not contain such placement. Make sure it has registered");

			IronSource.Agent.showRewardedVideo(rewarded);
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