using UnityEngine.AddressableAssets;

namespace Menu.Deck.Extensions
{
	public static class CardRepositoryExtensions
	{
		public static AssetReferenceGameObject ViewOf(this ICardRepository repository, ICard card)
		{
			return repository.ViewOf(card.VehicleId);
		}
	}
}