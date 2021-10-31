using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;

namespace CarSumo.Level
{
    public class VehicleDestroyerAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _soundEmitter;
        [SerializeField] private AudioCue _explosionCue;

        public void PlayExplosionSound(Collider collider)
        {
            _soundEmitter.transform.position = collider.transform.position;
            _soundEmitter.Play(_explosionCue);
        }
    }
}