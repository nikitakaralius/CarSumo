using System;
using CarSumo.DataModel.Accounts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuiBaseData.Accounts
{
    public abstract class AccountView : MonoBehaviour
    {
        [Header("Account View")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        public void ChangeAccount(Account account)
        {
            _name.text = account.Name.Value;
            _icon.sprite = account.Icon.Value.Sprite;
        }

        protected void ChangeViewValues(string name, Sprite icon)
        {
	        _name.text = name;
	        _icon.sprite = icon;
        }
    }
}