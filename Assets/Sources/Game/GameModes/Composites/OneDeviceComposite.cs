using Game.Mediation;

namespace Game.GameModes.Composites
{
	public class OneDeviceComposite : IGameComposite
	{
		public async void Compose(IMediator mediator)
		{
			await mediator.BootAsync();
			mediator.DeployAI();
		}
	}
}