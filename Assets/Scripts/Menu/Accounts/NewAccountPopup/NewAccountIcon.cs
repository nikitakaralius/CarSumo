using CarSumo.DataModel.Accounts;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Accounts
{
    public class NewAccountIcon : IAccountIconPresenter, IAccountIconReceiver, IInitializable
    {
        private const string DefaultIconPath = "Players/Icons/DefaultUserIcon.png";
        
        public IReadOnlyReactiveProperty<Icon> Icon => _icon;

        private readonly ReactiveProperty<Icon> _icon = new ReactiveProperty<Icon>();

        public void ReceiveIcon(Icon icon)
        {
            _icon.Value = icon;
        }

        public async void Initialize()
        {
            Sprite sprite = await Addressables.LoadAssetAsync<Sprite>(DefaultIconPath).Task;
            _icon.Value = new Icon(sprite, null);
        }
    }
}