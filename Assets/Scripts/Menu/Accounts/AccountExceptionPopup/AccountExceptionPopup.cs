using TMPro;
using UnityEngine;

namespace Menu.Accounts.AccountExceptionPopup
{
	public class AccountExceptionPopup : MonoBehaviour, IAccountExceptionPopup
	{
		[Header("Required components")] 
		[SerializeField] private TMP_Text _exceptionMessage;
		
		public void Show(string exceptionMessage)
		{
			_exceptionMessage.text = exceptionMessage;
		}
	}
}