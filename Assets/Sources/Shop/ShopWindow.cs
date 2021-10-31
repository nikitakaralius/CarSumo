using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using Menu.Tabs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shop
{
	public class ShopWindow : SerializedMonoBehaviour
	{
		[SerializeField] private IReadOnlyDictionary<ShopTab, TabElement> _tabs;

		public void OpenOnTab(ShopTab tab)
		{
			gameObject.SetActive(true);
			_tabs[tab].SetSelected(true);
		}
	}
}