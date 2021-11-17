using Advertisement.Units.Rewarded;
using CarSumo.DataModel.GameResources;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace UI
{
	public class UIMediator : MonoBehaviour
	{
		[Inject] private readonly IClientResourceOperations _resourceOperations;
		[Inject] private readonly IRewardedUnit _rewardedUnit;
		[Inject] private readonly GameStateMachine _stateMachine;
		
		[SerializeField] private ResourcesProvider _reward;
		[SerializeField, Min(1)] private int _adRewardModifier;
		[SerializeField, Min(1)] private int _energyToRestore;
		
		[Button] public void ReceiveReward() => ReceiveReward(1);
		
		[Button] public void ReceiveRewardWithAd() => _rewardedUnit.Show(RewardedPlacement.EndgameRewards, _ => ReceiveReward(_adRewardModifier));

		[Button] public void LoadMenu() => _stateMachine.Enter<AdvertisedMenuEntryState>();
		
		[Button] public void RestoreEnergyWithAd() => _rewardedUnit.Show(RewardedPlacement.EndgameEnergyRestore, _ => _resourceOperations.Receive(ResourceId.Energy, _energyToRestore));
		
		private void ReceiveReward(int modifier) => _reward.All().ForEach(reward => _resourceOperations.Receive(reward.Key, reward.Value * modifier));
	}
}