using UnityEngine;
using UnityEngine.Audio;

namespace CarSumo.Audio.AudioData
{
    [CreateAssetMenu(menuName = "Audio/Audio Configuration")]
    public class AudioConfigurationScriptableObject : ScriptableObject
    {
        [SerializeField] public AudioMixerGroup _outputAudioMixerGroup;

        [SerializeField] private PriorityLevel _priorityLevel = PriorityLevel.Standard;
		public int Priority
		{
			get => (int)_priorityLevel;
            set => _priorityLevel = (PriorityLevel)value;
        }

		[Header("Sound properties")]
		[SerializeField] private bool _mute;
		[Range(0f, 1f)] [SerializeField] private float _volume = 1f;
		[Range(-3f, 3f)] [SerializeField] private float _pitch = 1f;
		[Range(-1f, 1f)] [SerializeField] private float _panStereo = 0f;
		[Range(0f, 1.1f)] [SerializeField] private float _reverbZoneMix = 1f;

		[Header("Spatialisation")]
		[Range(0f, 1f)] [SerializeField] private float _spatialBlend = 1f;
        [SerializeField] private AudioRolloffMode _rolloffMode = AudioRolloffMode.Logarithmic;
		[Range(0.1f, 5f)] [SerializeField] private float _minDistance = 0.1f;
		[Range(5f, 1000f)] [SerializeField] private float _maxDistance = 50f;
		[Range(0, 360)] [SerializeField] private int _spread = 0;
		[Range(0f, 5f)] [SerializeField] private float _dopplerLevel = 1f;

		[Header("Ignores")]
        [SerializeField] private bool _bypassEffects = false;
        [SerializeField] private bool _bypassListenerEffects = false;
        [SerializeField] private bool _bypassReverbZones = false;
        [SerializeField] private bool _ignoreListenerVolume = false;
        [SerializeField] private bool _ignoreListenerPause = false;

		private enum PriorityLevel
		{
			Highest = 0,
			High = 64,
			Standard = 128,
			Low = 194,
			VeryLow = 256,
		}

		public void ApplyTo(AudioSource audioSource)
		{
			audioSource.outputAudioMixerGroup = _outputAudioMixerGroup;
			audioSource.mute = _mute;
			audioSource.bypassEffects = _bypassEffects;
			audioSource.bypassListenerEffects = _bypassListenerEffects;
			audioSource.bypassReverbZones = _bypassReverbZones;
			audioSource.priority = Priority;
			audioSource.volume = _volume;
			audioSource.pitch = _pitch;
			audioSource.panStereo = _panStereo;
			audioSource.spatialBlend = _spatialBlend;
			audioSource.reverbZoneMix = _reverbZoneMix;
			audioSource.dopplerLevel = _dopplerLevel;
			audioSource.spread = _spread;
			audioSource.rolloffMode = _rolloffMode;
			audioSource.minDistance = _minDistance;
			audioSource.maxDistance = _maxDistance;
			audioSource.ignoreListenerVolume = _ignoreListenerVolume;
			audioSource.ignoreListenerPause = _ignoreListenerPause;
		}
	}
}