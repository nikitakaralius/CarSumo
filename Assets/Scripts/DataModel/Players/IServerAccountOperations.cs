using UniRx;
using UnityEngine;

namespace CarSumo.DataModel.Accounts
{
    public interface IServerAccountOperations
    {
        bool TryChangeName(IReactiveProperty<string> currentName, string newName);
        void ChangeIcon(IReactiveProperty<Sprite> icon);
    }
}