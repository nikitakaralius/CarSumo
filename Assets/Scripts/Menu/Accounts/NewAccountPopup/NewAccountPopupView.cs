using CarSumo.DataModel.Accounts;
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
        
        private IAccountIconPresenter _iconPresenter;
        private IClientAccountStorageOperations _storageOperations;

        [Inject]
        private void Construct(IClientAccountStorageOperations storageOperations, IAccountIconPresenter iconPresenter)
        {
            _storageOperations = storageOperations;
            _iconPresenter = iconPresenter;
        }

        private void AddNewAccount()
        {
            
        }
    }
}