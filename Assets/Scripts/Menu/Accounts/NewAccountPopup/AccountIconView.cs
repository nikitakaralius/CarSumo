using CarSumo.DataModel.Accounts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Image))]
    public class AccountIconView : MonoBehaviour
    {
        private Image _image;
        private IAccountIconPresenter _iconPresenter;

        [Inject]
        private void Construct(IAccountIconPresenter iconPresenter)
        {
            _iconPresenter = iconPresenter;
        }
        
        private void Start()
        {
            _image = GetComponent<Image>();
            _iconPresenter.Icon.Subscribe(ChangeImage);
        }

        private void ChangeImage(Icon icon)
        {
            _image.sprite = icon.Sprite;
        }
    }
}