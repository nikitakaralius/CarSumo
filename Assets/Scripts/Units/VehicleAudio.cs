using UnityEngine;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;

namespace CarSumo.Units
{
    public class VehicleAudio : MonoBehaviour
    {
        [SerializeField] private AudioCue _collisionCue;
        [SerializeField] private MonoSoundEmitter _soundEmitter;

        public void PlayCollisionSound()
        {
            _soundEmitter.Play(_collisionCue);
        }
    }
}