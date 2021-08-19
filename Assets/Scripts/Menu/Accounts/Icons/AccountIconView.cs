using System;
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
        [SerializeField] private Sprite _defaultImage;
        
        private Image _image;
        private IAccountIconPresenter _iconPresenter;
        private IDisposable _subscription;

        [Inject]
        private void Construct(IAccountIconPresenter iconPresenter)
        {
            _iconPresenter = iconPresenter;
        }
        
        private void Start()
        {
            _image = GetComponent<Image>();
            _subscription = _iconPresenter.Icon.Subscribe(ChangeImage);
        }

        private void OnDisable()
        {
            _image.sprite = _defaultImage;
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }

        private void ChangeImage(Icon icon)
        {
            _image.sprite = icon.Sprite;
        }
    }
}