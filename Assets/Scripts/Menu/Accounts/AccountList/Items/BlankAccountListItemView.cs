using AdvancedAudioSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Button))]
    public class BlankAccountListItemView : MonoBehaviour
    {
        private IAccountPopup _accountPopup;
        private IAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(IAccountPopup accountPopup, IAudioPlayer audioPlayer)
        {
            _accountPopup = accountPopup;
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(_accountPopup.Show);
            button.onClick.AddListener(_audioPlayer.Play);
        }
    }
}