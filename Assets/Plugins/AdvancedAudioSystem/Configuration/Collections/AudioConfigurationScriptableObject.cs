using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;

namespace AdvancedAudioSystem.Configuration
{
    [CreateAssetMenu(fileName = "AudioConfiguration", menuName = "Audio/AudioConfigurationAsset")]
    public class AudioConfigurationScriptableObject : SerializedScriptableObject, IAudioConfigurationEnumerable
    {
        [SerializeField] private List<IAudioConfiguration> _configurations = new List<IAudioConfiguration>(0);

        public IEnumerator<IAudioConfiguration> GetEnumerator()
        {
            return _configurations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
