using Advertisement.Units.Interstitial;
using Zenject;

namespace CarSumo.StateMachine.States
{
	public class AdvertisedMenuEntryState : IState
	{
		private readonly LazyInject<GameStateMachine> _stateMachine;
		private readonly IInterstitialUnit _interstitial;

		public AdvertisedMenuEntryState(LazyInject<GameStateMachine> stateMachine, IInterstitialUnit interstitial)
		{
			_stateMachine = stateMachine;
			_interstitial = interstitial;
		}

		public void Enter()
		{
			_interstitial.Show(InterstitialPlacement.BattleComplete);
			_stateMachine.Value.Enter<MenuEntryState>();
		}

		public void Exit()
		{
		}
	}
}