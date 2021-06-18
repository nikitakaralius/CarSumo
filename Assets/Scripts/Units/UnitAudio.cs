using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;

namespace CarSumo.Units
{
    [RequireComponent(typeof(Unit))]
    public class UnitAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _soundEmitter;
        [SerializeField] private AudioCue _upgradeCue;

        private Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void OnEnable()
        {
            _unit.Upgraded += PlayUpgradeSound;
        }

        private void OnDisable()
        {
            _unit.Upgraded -= PlayUpgradeSound;
        }

        private void PlayUpgradeSound()
        {
            _soundEmitter.Play(_upgradeCue);
        }
    }
}