namespace CarSumo.DataModel.Accounts
{
    public interface IClientAccountStorageOperations
    {
        bool TryAddAccount(Account account);
    }
}