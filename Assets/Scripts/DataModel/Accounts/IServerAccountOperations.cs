using UniRx;
using UnityEngine;

namespace CarSumo.DataModel.Accounts
{
    public interface IServerAccountOperations
    {
        bool TryChangeName(Account account, string newName);
        void ChangeIcon(Account account, Sprite icon);
    }
}