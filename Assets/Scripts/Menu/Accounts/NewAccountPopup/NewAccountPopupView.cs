using System;
using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    public class NewAccountPopupView : SerializedMonoBehaviour
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private TMP_InputField _nameInputField;

        private IVehicleLayoutBuilder _layoutBuilder;
        private IAccountIconPresenter _iconPresenter;
        private IClientAccountStorageOperations _storageOperations;
        private IClientAccountOperations _accountOperations;
        private IAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(IClientAccountStorageOperations storageOperations,
                               IAccountIconPresenter iconPresenter,
                               IClientAccountOperations accountOperations,
                               IVehicleLayoutBuilder layoutBuilder,
                               IAudioPlayer audioPlayer)
        {
            _storageOperations = storageOperations;
            _accountOperations = accountOperations;
            _iconPresenter = iconPresenter;
            _layoutBuilder = layoutBuilder;
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            _createButton.onClick.AddListener(AddAndActivateNewAccount);
            _createButton.onClick.AddListener(_audioPlayer.Play);
        }

        private void AddAndActivateNewAccount()
        {
            Account account = CreateAccount();
            
            if (_storageOperations.TryAddAccount(account))
            {
                HidePopup();
                _accountOperations.SetActive(account);
            }
            else
            {
                throw new InvalidOperationException($"Can not add new account {account}");
            }
        }

        private void HidePopup()
        {
            gameObject.SetActive(false);
        }

        private Account CreateAccount()
        {
            return new Account(_nameInputField.text,
                _iconPresenter.Icon.Value,
                _layoutBuilder.Create(new DefaultVehicleLayout()));
        }
    }
}