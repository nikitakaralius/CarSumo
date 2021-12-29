using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu.Deck
{
	public interface IPlacement
	{
		GameObject Add(AssetReferenceGameObject gameObject);
	}
}