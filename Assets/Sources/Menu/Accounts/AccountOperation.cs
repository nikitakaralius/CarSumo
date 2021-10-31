using CarSumo.DataModel.Accounts;

namespace Menu.Accounts
{
	public readonly struct AccountOperation
	{
		public string ExceptionMessage { get; }
		public Account Account { get; }

		public AccountOperation(string exceptionMessage, Account account)
		{
			ExceptionMessage = exceptionMessage;
			Account = account;
		}

		public bool Valid => ExceptionMessage is null;
	}
}