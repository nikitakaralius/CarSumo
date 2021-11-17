using AdvancedAudioSystem;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Gameplay.Pause
{
    [RequireComponent(typeof(Button))]
    public class ClosePauseWindowButton : MonoBehaviour
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
            button.onClick.AddListener(ExitPauseState);
            button.onClick.AddListener(_audioPlayer.Play);
        }

        private void ExitPauseState()
        {
            _pausePopup.SetActive(false);
            _stateMachine.Enter<GameState>();
        }
    }
}