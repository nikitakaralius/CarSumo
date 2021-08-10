using System;
using AdvancedAudioSystem;
using DataModel.Vehicles;
using TweenAnimations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Cards
{
    [RequireComponent(typeof(Button))]
    public class VehicleCard : MonoBehaviour
    {
        [SerializeField] private VehicleId _id;
        [SerializeField] private SizeDeltaTweenAnimation _animation;

        private IAudioPlayer _audioPlayer;

        private Button _button;
        private bool _clicked = false;

        public VehicleId Id => _id;

        [Inject]
        private void Construct(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Initialize(Action<bool> onButtonClicked)
        {
            _button ??= GetComponent<Button>();

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnButtonClickedInternal(onButtonClicked));
        }
        
        private void OnButtonClickedInternal(Action<bool> onButtonClicked)
        {
            _clicked = _clicked == false;
            _audioPlayer.Play();
            onButtonClicked.Invoke(_clicked);

            if (_clicked)
                _animation.IncreaseSize();
            else
                _animation.DecreaseSize();
        }
    }
}