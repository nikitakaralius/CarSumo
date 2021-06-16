using System;
using System.Collections;
using UnityEngine;

namespace CarSumo.Audio.AudioData.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public class MonoSoundEmitter : MonoBehaviour
    {
        public event Action<MonoSoundEmitter> FinishedPlaying;

        public bool IsPlaying => _audioSource.isPlaying;
        public bool IsLooping => _audioSource.loop;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudioClip(AudioClip clip, AudioConfigurationScriptableObject configuration, bool hasToLoop)
        {
            _audioSource.clip = clip;
            configuration.ApplyTo(_audioSource);
            _audioSource.transform.position = transform.position;
            _audioSource.loop = hasToLoop;
            _audioSource.time = 0.0f;

            _audioSource.Play();

            if (!hasToLoop)
                StartCoroutine(FinishPlaying(clip.length));
        }

        public void Resume() => _audioSource.Play();
        public void Pause() => _audioSource.Pause();
        public void Stop() => _audioSource.Stop();

        public void Finish()
        {
            if (_audioSource.loop == false)
                return;

            _audioSource.loop = false;
            var timeRemaining = _audioSource.clip.length - _audioSource.time;
            StartCoroutine(FinishPlaying(timeRemaining));
        }

        private IEnumerator FinishPlaying(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);

            FinishedPlaying?.Invoke(this);
        }
    }
}