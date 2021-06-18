using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class AssetAudioConfiguration : IAudioConfigurationEnumerable
    {
        [SerializeField] private AudioConfigurationScriptableObject _configuration;

        public IEnumerator<IAudioConfiguration> GetEnumerator()
        {
            return _configuration.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
