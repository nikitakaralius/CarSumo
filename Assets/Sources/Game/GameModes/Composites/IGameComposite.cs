using Game.Mediation;

namespace Game.GameModes.Composites
{
	public interface IGameComposite
	{
		void Compose(IMediator mediator);
	}
}