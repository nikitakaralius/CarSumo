using System;
using System.Collections;
using UnityEngine;

namespace AdvancedAudioSystem.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public class MonoSoundEmitter : MonoBehaviour, ISoundEmitter
    {
        public event Action FinishedPlaying;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Play(AudioCue audioCue)
        {
            audioCue.PlayOn(_audioSource);

            if (_audioSource.loop)
                return;

            StartCoroutine(FinishPlaying(_audioSource.clip.length));
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        private IEnumerator FinishPlaying(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);

            FinishedPlaying?.Invoke();
        }
    }
}
