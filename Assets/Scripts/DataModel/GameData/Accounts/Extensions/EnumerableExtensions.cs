using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;

namespace DataModel.GameData.Accounts.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task<IEnumerable<Account>> ToBoundAccountsAsync(this IEnumerable<SerializableAccount> accounts,
                                                                         IAsyncAccountBinding accountBinding)
        {
            return await ToOtherAccountsAsync(accounts, accountBinding.ToAccountAsync);
        }

        public static async Task<IEnumerable<SerializableAccount>> ToSerializableAccountsAsync(this IEnumerable<Account> accounts,
                                                                                            IAsyncAccountBinding accountBinding)
        {
            return await ToOtherAccountsAsync(accounts, accountBinding.ToSerializableAccountAsync);
        }

        private static async Task<IEnumerable<TTo>> ToOtherAccountsAsync<TTo, TFrom>(IEnumerable<TFrom> accounts,
                                                                                    Func<TFrom, Task<TTo>> binding)
        {
            var otherAccounts = new List<TTo>();
            foreach (TFrom account in accounts)
            {
                TTo otherAccount = await binding.Invoke(account);
                otherAccounts.Add(otherAccount);
            }
            return otherAccounts;
        }
    }
}