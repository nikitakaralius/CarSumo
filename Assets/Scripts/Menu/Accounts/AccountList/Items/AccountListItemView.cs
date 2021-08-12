using GuiBaseData.Accounts;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Accounts
{
	public class AccountListItemView : AccountView
	{
		[Header("Account List Item View")] 
		[SerializeField] private Image _backgroundImage;
		[SerializeField] private Sprite _selectedBackground;
		[SerializeField] private Sprite _deselectedBackground;

		public void ConfigureBackground(bool accountActive)
		{
			_backgroundImage.sprite = accountActive ? _selectedBackground : _deselectedBackground;
		}
	}
}