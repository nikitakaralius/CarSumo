using System;
using System.Collections;
using UnityEngine;

namespace AdvancedAudioSystem.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public class MonoSoundEmitter : MonoBehaviour, ISoundEmitter
    {
        public event Action FinishedPlaying;

        public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
        }

        public void Play(AudioCue audioCue)
        {
            audioCue.PlayOn(AudioSource);

            if (AudioSource.loop)
                return;

            StartCoroutine(FinishPlaying(AudioSource.clip.length));
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        private IEnumerator FinishPlaying(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);

            FinishedPlaying?.Invoke();
        }
    }    
}
