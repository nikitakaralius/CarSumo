using CarSumo.Audio;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
	public class BackgroundMusicInstaller : MonoInstaller
	{
		[SerializeField] private BackgroundMusic _backgroundMusicSceneObject;

		public override void InstallBindings()
		{
			BindBackgroundMusic();
		}

		private void BindBackgroundMusic()
		{
			Container
				.Bind<BackgroundMusic>()
				.FromInstance(_backgroundMusicSceneObject)
				.AsSingle();
		}
	}
}