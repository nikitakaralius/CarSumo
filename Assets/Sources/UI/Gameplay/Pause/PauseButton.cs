using AdvancedAudioSystem;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Gameplay.Pause
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePopup;

        private GameStateMachine _stateMachine;
        private IAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(GameStateMachine stateMachine, IAudioPlayer audioPlayer)
        {
            _stateMachine = stateMachine;
            _audioPlayer = audioPlayer;
        }
        
        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(EnterPauseState);
            button.onClick.AddListener(_audioPlayer.Play);
        }
        
        private void EnterPauseState()
        {
            _pausePopup.SetActive(true);
            _stateMachine.Enter<PauseState>();
        }
    }
}