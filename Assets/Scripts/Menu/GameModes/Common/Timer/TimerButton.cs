using AdvancedAudioSystem;
using Menu.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.GameModes.Common.Timer
{
	[RequireComponent(typeof(Image))]
	public class TimerButton : SelectableButton<ITimerButtonSelectHandler, TimerButton>
	{
		[Header("Graphics")]
		[SerializeField] private Sprite _selectedBackground;
		[SerializeField] private Sprite _deselectedBackground;

		[Header("Preferences")] 
		[Range(0.0f, float.MaxValue)]
		[SerializeField] private float _timeAmount;

		private IAudioPlayer _audioPlayer;
		private Image _image;

		public float TimeAmount => _timeAmount;

		[Inject]
		private void Construct(IAudioPlayer audioPlayer)
		{
			_audioPlayer = audioPlayer;
		}
		
		public void Initialize(ITimerButtonSelectHandler selectHandler)
		{
			_image = GetComponent<Image>();
			
			Initialize(this, selectHandler);
		}
		
		protected override void OnButtonSelectedInternal()
		{
			_image.sprite = _selectedBackground;
			_audioPlayer.Play();
		}

		protected override void OnButtonDeselectedInternal()
		{
			_image.sprite = _deselectedBackground;
		}
	}
}