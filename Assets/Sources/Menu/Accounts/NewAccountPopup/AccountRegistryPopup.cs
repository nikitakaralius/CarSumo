using CarSumo.DataModel.Accounts;
using Menu.Accounts.AccountEditor;
using Menu.Accounts.AccountExceptionPopup;
using Sirenix.OdinInspector;
using UnityEngine;
using ButtonTitle = Menu.Accounts.AccountChangerButton.ButtonTitle;

namespace Menu.Accounts
{
    public class AccountRegistryPopup : SerializedMonoBehaviour, INewAccountPopup, IAccountEditorPopup
    {
	    [Header("Required Components")]
	    [SerializeField] private AccountChangerButton _changerButton;
	    [SerializeField] private RemoveAccountButton _removeButton;
	    [SerializeField] private INewAccountRecorder _newAccountRecorder;
	    [SerializeField] private IAccountExceptionPopup _exceptionPopup;
	    [SerializeField] private IAccountEditor _accountEditor;
	    [SerializeField] private GameObject _openRemoveDialogueButton;
	    
	    public void Open()
	    {
		    gameObject.SetActive(true);
		    
		    _changerButton.ChangeOnButtonClickedSubscription(AddNewAccount, ButtonTitle.NewAccount);
	    }

	    public void Open(Account account)
	    {
		    _accountEditor.SetInitialAccountValues(account);
		    
		    _changerButton.ChangeOnButtonClickedSubscription(() =>
			    ChangeExistingAccount(account), ButtonTitle.ChangeAccount);
		    
		    gameObject.SetActive(true);
		    _openRemoveDialogueButton.SetActive(true);
		    _removeButton.ChangeOnButtonClicked(() => RemoveAccount(account));
	    }

	    public void Close()
	    {
		    gameObject.SetActive(false);
		    _openRemoveDialogueButton.SetActive(false);
	    }

	    private bool AddNewAccount()
	    {
		    AccountOperation operation = _newAccountRecorder.RecordNewAccount();
		    
		    return HandleOperationMessages(ref operation);
	    }

	    private bool ChangeExistingAccount(Account account)
	    {
		    AccountOperation operation = _accountEditor.ChangeAccountValues(account);
		    
		    return HandleOperationMessages(ref operation);
	    }

	    private bool RemoveAccount(Account account)
	    {
		    AccountOperation operation = _accountEditor.RemoveAccount(account);

		    return HandleOperationMessages(ref operation);
	    }

	    private bool HandleOperationMessages(ref AccountOperation operation)
	    {
		    if (operation.Valid)
			    return true;
		    
		    _exceptionPopup.Show(operation.ExceptionMessage);
		    return false;
	    }
    }
}