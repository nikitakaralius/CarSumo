using Sirenix.OdinInspector;
using UnityEngine;

namespace Menu.Cards
{
	public class ScaledPlacement : SerializedMonoBehaviour, IPlacement
	{
		[SerializeField] private IPlacement _placement;
		[SerializeField] private Vector3 _elementScale = Vector3.one;
		
		public GameObject Add(GameObject element)
		{
			element.transform.localScale = _elementScale;
			_placement.Add(element);
			return element;
		}

		public GameObject AddFromPrefab(GameObject prefab)
		{
			GameObject element = _placement.AddFromPrefab(prefab);
			element.transform.localScale = _elementScale;
			return element;
		}

		public void Show()
		{
			_placement.Show();
		}

		public void Hide()
		{
			_placement.Hide();
		}
	}
}