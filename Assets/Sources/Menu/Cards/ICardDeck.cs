using System.Collections.Generic;

namespace Menu.Cards
{
	public interface ICardDeck
	{
		IEnumerable<CardInDeck> Cards { get; }
	}
}