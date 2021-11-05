using Game.Mediation;

namespace Game.GameModes.Composites
{
	public class OneDeviceComposite : IGameComposite
	{
		public void Compose(IMediator mediator)
		{
			mediator.BootAsync();
		}
	}
}