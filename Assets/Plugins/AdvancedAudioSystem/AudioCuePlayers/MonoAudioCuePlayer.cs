using UnityEngine;
using AdvancedAudioSystem.Emitters;

namespace AdvancedAudioSystem
{
    [RequireComponent(typeof(ISoundEmitter))]
    public class MonoAudioCuePlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private AudioCue _audioCue;

        private ISoundEmitter _emitter;

        private void Awake()
        {
            _emitter = GetComponent<ISoundEmitter>();
        }

        public void Play() => _emitter.Play(_audioCue);
        public void Stop() => _emitter.Stop();
    }
}
