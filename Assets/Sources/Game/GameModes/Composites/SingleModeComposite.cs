using CarSumo.Teams;
using Game.Mediation;

namespace Game.GameModes.Composites
{
	public class SingleModeComposite : IGameComposite
	{
		public async void Compose(IMediator mediator)
		{
			await mediator.BootAsync();
			mediator.RememberTeamCameraPosition(Team.Blue, remember: false);
			mediator.DeployAI();
		}
	}
}