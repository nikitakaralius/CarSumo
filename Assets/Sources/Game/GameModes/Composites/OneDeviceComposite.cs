using Game.Endgame;
using Game.Mediation;
using Game.Rules;

namespace Game.GameModes.Composites
{
	public class OneDeviceComposite : IGameComposite
	{
		public void Compose(IMediator mediator)
		{
			mediator.BootAsync();
			mediator.RememberTeamCameraPosition(null, true);
			mediator.ChooseRules<OneDeviceMode.PickerRules>();
			mediator.ConfigureEndgame<OneDeviceStatusProvider>();
		}
	}
}