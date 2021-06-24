using UnityEngine;
using AdvancedAudioSystem.Emitters;

namespace AdvancedAudioSystem
{
    [CreateAssetMenu(fileName = "Audio Cue Player", menuName = "Audio/AudioCuePlayerAsset")]
    public class AudioCuePlayerScriptableObject : ScriptableObject
    {
        [SerializeField] private MonoSoundEmitter _soundEmitterPrefab;
        [SerializeField] private AudioCue _audioCue;

        public void PlayOn(Transform parent)
        {
            var instance = Instantiate(_soundEmitterPrefab, parent);
            instance.Play(_audioCue);
            instance.FinishedPlaying += () => Destroy(instance.gameObject);
        }
    }
}
