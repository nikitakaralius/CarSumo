using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;

namespace CarSumo.GUI.Processees
{
    public class GUIAudioProcess : IGUIProcess
    {
        [SerializeField] private ISoundEmitter _soundEmitter;
        [SerializeField] private AudioCue _audioCue;

        public GUIAudioProcess(ISoundEmitter soundEmitter, AudioCue audioCue)
        {
            _soundEmitter = soundEmitter;
            _audioCue = audioCue;
        }

        public void Init()
        {
        }

        public void Apply()
        {
            _soundEmitter.Play(_audioCue);
        }

        public void Stop()
        {
            _soundEmitter.Stop();
        }
    }
}
