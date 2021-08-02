using System.Collections.Generic;
using AdvancedAudioSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Players.Models.Binders
{
    public class ProfileIconsGroupBinder : SerializedMonoBehaviour
    {
        [SerializeField] private Icon _userIcon;
        [SerializeField] private IAudioPlayer _clickSoundPlayer;
        
        private void Start()
        {
            IEnumerable<IconBinder> binders = GetComponentsInChildren<IconBinder>();

            foreach (IconBinder binder in binders)
            {
                binder.Bind(_userIcon, _clickSoundPlayer);
            }
        }
    }
}