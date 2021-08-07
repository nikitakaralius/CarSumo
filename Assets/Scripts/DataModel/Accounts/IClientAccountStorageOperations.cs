using System.Collections.Generic;

namespace CarSumo.DataModel.Accounts
{
    public interface IClientAccountStorageOperations
    {
        bool TryAddAccount(Account account);
        void ChangeOrder(IReadOnlyList<Account> order);
    }
}