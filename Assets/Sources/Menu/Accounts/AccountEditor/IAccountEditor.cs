using CarSumo.DataModel.Accounts;

namespace Menu.Accounts.AccountEditor
{
	public interface IAccountEditor
	{
		void SetInitialAccountValues(Account account);
		AccountOperation RemoveAccount(Account account);
		AccountOperation ChangeAccountValues(Account account);
	}
}