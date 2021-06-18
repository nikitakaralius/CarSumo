using UnityEngine;
using Sirenix.OdinInspector;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;

namespace CarSumo.Units
{
    public class UnitAudio : SerializedMonoBehaviour
    {
        [SerializeField] private ISoundEmitter _soundEmitter;

        [SerializeField] private AudioCue _destroyCue;

        private Unit _unit;

        private void OnEnable()
        {
            _unit = GetComponent<Unit>();
            _unit.Destroying += PlayDestroySound;
        }

        private void OnDisable()
        {
            _unit.Destroying -= PlayDestroySound;
        }

        private void PlayDestroySound(Unit unit)
        {
            _soundEmitter.Play(_destroyCue);
        }
    }
}
