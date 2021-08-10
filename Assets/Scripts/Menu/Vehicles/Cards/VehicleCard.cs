using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedAudioSystem;
using DataModel.Vehicles;
using Sirenix.Utilities;
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

        private Transform _contentRoot;
        private Transform _selectedRoot;
        private LayoutGroup _layoutGroup;
        private IEnumerable<VehicleCard> _layout;

        private Button _button;
        
        private bool _clicked = false;
        private int _siblingIndex;

        public VehicleId Id => _id;

        [Inject]
        private void Construct(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Initialize(Transform contentRoot,
                                Transform selectedRoot,
                                LayoutGroup layoutGroup,
                                Action<bool> onButtonClicked,
                                IEnumerable<VehicleCard> layout)
        {
            _layout = layout;
            _contentRoot = contentRoot;
            _selectedRoot = selectedRoot;
            _layoutGroup = layoutGroup;

            _button ??= GetComponent<Button>();

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnButtonClickedInternal(onButtonClicked));
        }

        private void OnDisable()
        {
            _clicked = false;
            _layoutGroup.enabled = true;
            transform.SetParent(_contentRoot);
            transform.SetSiblingIndex(_siblingIndex);
            _animation.DecreaseSize();
        }

        private void SetInactive()
        {
            _clicked = false;
            
            transform.SetParent(_contentRoot);
            transform.SetSiblingIndex(_siblingIndex);
            _animation.DecreaseSize();
        }
        
        private void OnButtonClickedInternal(Action<bool> onButtonClicked)
        {
            _clicked = _clicked == false;
            _audioPlayer.Play();
            onButtonClicked.Invoke(_clicked);

            if (_clicked)
            {
                _siblingIndex = transform.GetSiblingIndex();

                _layoutGroup.enabled = false;
                transform.SetParent(_selectedRoot);
                
                _animation.IncreaseSize();
                NotifyOtherCardsBeingClicked();
            }
            else
            {
                _layoutGroup.enabled = true;
                transform.SetParent(_contentRoot);
                transform.SetSiblingIndex(_siblingIndex);
                
                _animation.DecreaseSize();
            }
        }

        private void NotifyOtherCardsBeingClicked()
        {
            _layout
                .Where(x => x != this)
                .ForEach(x => x.SetInactive());
        }
    }
}