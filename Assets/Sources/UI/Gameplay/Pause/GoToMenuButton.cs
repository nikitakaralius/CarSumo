using AdvancedAudioSystem;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Gameplay.Pause
{
    [RequireComponent(typeof(Button))]
    public class GoToMenuButton : MonoBehaviour
    {
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
            GetComponent<Button>()
                .onClick
                .AddListener(EnterMenu);
        }

        private void EnterMenu()
        {
            _audioPlayer.Play();
            _stateMachine.Enter<AdvertisedMenuEntryState>();
        }
    }
}