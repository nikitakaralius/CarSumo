using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class GroupPlacement : MonoBehaviour, IPlacement
	{
		[SerializeField] private Transform _root;

		private DiContainer _container;

		[Inject]
		private void Construct(DiContainer container)
		{
			_container = container;
		}
		
		public GameObject Add(GameObject element)
		{
			element.transform.SetParent(_root);
			return element;
		}

		public GameObject AddFromPrefab(GameObject prefab)
		{ 
			GameObject element = _container.InstantiatePrefab(prefab);
			Add(element);
			return element;
		}

		public void Show()
		{
			_root.gameObject.SetActive(true);
		}

		public void Hide()
		{
			_root.gameObject.SetActive(false);
		}
	}
}