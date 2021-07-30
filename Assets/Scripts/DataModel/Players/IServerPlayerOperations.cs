using UniRx;
using UnityEngine;

namespace CarSumo.DataModel.Players
{
    public interface IServerPlayerOperations
    {
        bool TryChangeName(IReactiveProperty<string> currentName, string newName);
        void ChangeIcon(IReactiveProperty<Sprite> icon);
    }
}