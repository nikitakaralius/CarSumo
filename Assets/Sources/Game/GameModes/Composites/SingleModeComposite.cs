using CarSumo.Teams;
using Game.Mediation;
using Game.Rules;

namespace Game.GameModes.Composites
{
	public class SingleModeComposite : IGameComposite
	{
		public async void Compose(IMediator mediator)
		{
			await mediator.BootAsync();
			mediator.RememberTeamCameraPosition(Team.Blue, remember: false);
			mediator.DeployAI();
			mediator.ConfigureSelector<SingleMode.PickerRules>();
		}
	}
}