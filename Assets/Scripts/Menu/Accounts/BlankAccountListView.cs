using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Button))]
    public class BlankAccountListView : MonoBehaviour
    {
        private IAccountPopup _accountPopup;

        [Inject]
        private void Construct(IAccountPopup accountPopup)
        {
            _accountPopup = accountPopup;
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(_accountPopup.Show);
        }
    }
}