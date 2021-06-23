using UnityEngine;
using AdvancedAudioSystem.Emitters;

namespace AdvancedAudioSystem
{
    [RequireComponent(typeof(MonoSoundEmitter))]
    public class MonoAudioCuePlayer : MonoBehaviour
    {
        [SerializeField] private AudioCue _audioCue;

        private MonoSoundEmitter _emitter;

        private void Awake()
        {
            _emitter = GetComponent<MonoSoundEmitter>();
        }

        public void Play() => _emitter.Play(_audioCue);
        public void Stop() => _emitter.Stop();
    }
}
