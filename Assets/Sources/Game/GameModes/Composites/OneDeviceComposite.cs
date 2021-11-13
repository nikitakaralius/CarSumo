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
			mediator.ConfigureSelector<OneDeviceMode.PickerRules>();
		}
	}
}