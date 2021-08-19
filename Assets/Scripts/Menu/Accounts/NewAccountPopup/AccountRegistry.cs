using CarSumo.DataModel.Accounts;
using Menu.Accounts.AccountEditor;
using Menu.Accounts.AccountExceptionPopup;
using Sirenix.OdinInspector;
using UnityEngine;
using ButtonTitle = Menu.Accounts.AccountChangerButton.ButtonTitle;

namespace Menu.Accounts
{
    public class AccountRegistry : SerializedMonoBehaviour, INewAccountPopup, IAccountEditorPopup
    {
	    [SerializeField] private AccountChangerButton _button;
	    [SerializeField] private INewAccountRecorder _newAccountRecorder;
	    [SerializeField] private IAccountExceptionPopup _exceptionPopup;
	    [SerializeField] private IAccountEditor _accountEditor;
	    
	    public void Open()
		    => _button.ChangeOnButtonClickedSubscription(AddNewAccount, ButtonTitle.NewAccount);

	    public void Open(Account account)
		    => _button.ChangeOnButtonClickedSubscription(() =>
			    ChangeExistingAccount(account), ButtonTitle.ChangeAccount);

	    public void Close()
		    => gameObject.SetActive(false);

	    private void AddNewAccount()
	    {
		    AccountOperation operation = _newAccountRecorder.RecordNewAccount();
		    
		    HandleOperationMessages(ref operation);
	    }

	    private void ChangeExistingAccount(Account account)
	    {
		    AccountOperation operation = _accountEditor.ChangeAccountValues(account);
		    
		    HandleOperationMessages(ref operation);
	    }

	    private void HandleOperationMessages(ref AccountOperation operation)
	    {
		    if (operation.Valid)
			    return;
		    
		    _exceptionPopup.Show(operation.ExceptionMessage);
	    }
    }
}