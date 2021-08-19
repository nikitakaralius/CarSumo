using CarSumo.DataModel.Accounts;

namespace Menu.Accounts.AccountEditor
{
	public interface IAccountEditor
	{
		AccountOperation ChangeAccount(Account account);
	}
}