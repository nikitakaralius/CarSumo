using System;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Audio
{
	public class GameBackgroundMusic : MonoBehaviour
	{
		[Header("Cues")]
		[SerializeField] private AudioCue _backgroundMusic;
		[SerializeField] private AudioCue _winMusic;

		[Header("Sound Emitter")]
		[SerializeField] private MonoSoundEmitter _soundEmitter;

		private IWinMessage _winMessage;

		private IDisposable _winSubscription;
		private AudioCue _playingCue;

		[Inject]
		private void Construct(IWinMessage winMessage)
		{
			_winMessage = winMessage;
		}
		
		private void OnEnable()
		{
			_playingCue = _backgroundMusic;
			
			_soundEmitter.FinishedPlaying += PlayNextClip;

			_winSubscription = _winMessage
				.ObserveWin()
				.Subscribe(_ =>
				{
					_soundEmitter.Stop();
					_playingCue = _winMusic;
					PlayNextClip();
				});
		}

		private void OnDisable()
		{
			_soundEmitter.FinishedPlaying -= PlayNextClip;
			
			_winSubscription.Dispose();
		}

		private void PlayNextClip()
		{
			_soundEmitter.Play(_playingCue);
		}
	}
}