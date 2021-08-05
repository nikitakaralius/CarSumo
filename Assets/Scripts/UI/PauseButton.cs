using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePopup;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(EnterPauseState);
        }
        
        public void EnterPauseState()
        {
            _pausePopup.SetActive(true);
            Time.timeScale = 0;
        }

        public void ExitPauseState()
        {
            Time.timeScale = 1;
        }
    }
}