using CarSumo.DataModel.Accounts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuiBaseData.Accounts
{
    public class AccountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        public void ChangeAccount(Account account)
        {
            _name.text = account.Name.Value;
            _icon.sprite = account.Icon.Value.Sprite;
        }

        protected void ChangeViewValues(string accountName, Sprite icon)
        {
	        _name.text = accountName;
	        _icon.sprite = icon;
        }
    }
}