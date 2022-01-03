using UnityEngine;

namespace Menu.Cards
{
	public interface IPlacement
	{
		GameObject Add(GameObject element);
		GameObject AddFromPrefab(GameObject prefab);
		void Show();
		void Hide();
	}
}