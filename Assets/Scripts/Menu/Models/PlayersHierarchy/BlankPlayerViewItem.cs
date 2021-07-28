using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.Menu.Models
{
    [RequireComponent(typeof(Button))]
    public class BlankPlayerViewItem : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Init(GameObject newUserWindow)
        {
            _button.onClick.AddListener(() => newUserWindow.SetActive(true));
        }
    }
}