using System;
using System.Collections;
using UnityEngine;

namespace AdvancedAudioSystem.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public class MonoSoundEmitter : MonoBehaviour, ISoundEmitter
    {
        private AudioSource _audioSource;
        private Coroutine _finishPlayingRoutine;
        
        public event Action FinishedPlaying;

        public IAudioSourceProperty AudioSourceProperty { get; private set; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            AudioSourceProperty = new AudioSourceProperty(_audioSource);
        }

        public void Play(AudioCue audioCue)
        {
	        if (_finishPlayingRoutine != null)
		        StopCoroutine(_finishPlayingRoutine);
	        
            audioCue.PlayOn(_audioSource);

            if (_audioSource.loop)
                return;

            _finishPlayingRoutine = StartCoroutine(FinishPlaying(_audioSource.clip.length));
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
