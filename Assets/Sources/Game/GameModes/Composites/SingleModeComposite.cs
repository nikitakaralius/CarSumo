using Game.Mediation;

namespace Game.GameModes.Composites
{
	public class SingleModeComposite : IGameComposite
	{
		public void Compose(IMediator mediator)
		{
			mediator.BootAsync();
		}
	}
}