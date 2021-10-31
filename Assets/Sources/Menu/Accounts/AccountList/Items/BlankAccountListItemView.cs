using AdvancedAudioSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Button))]
    public class BlankAccountListItemView : MonoBehaviour
    {
        private INewAccountPopup _accountPopup;
        private IAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(INewAccountPopup accountPopup, IAudioPlayer audioPlayer)
        {
            _accountPopup = accountPopup;
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(_accountPopup.Open);
            button.onClick.AddListener(_audioPlayer.Play);
        }
    }
}