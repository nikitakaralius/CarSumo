using System.Collections.Generic;

namespace CarSumo.DataModel.Accounts
{
    public class SerializableAccountStorage
    {
        public IEnumerable<SerializableAccount> AllAccounts { get; set; }
        
        public SerializableAccount ActiveAccount { get; set; }
    }
}