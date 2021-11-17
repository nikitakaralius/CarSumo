using System;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using Game.Endgame;
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

		private IEndGameMessage _endGameMessage;

		private IDisposable _winSubscription;
		private AudioCue _playingCue;

		[Inject]
		private void Construct(IEndGameMessage endGameMessage)
		{
			_endGameMessage = endGameMessage;
		}
		
		private void Start()
		{
			_playingCue = _backgroundMusic;
			
			PlayNextClip();
			
			_soundEmitter.FinishedPlaying += PlayNextClip;

			_winSubscription = _endGameMessage
				.ObserveEnding()
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