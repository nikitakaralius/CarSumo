using CarSumo.Coroutines;
using GameModes;
using Services.Timer.InGameTimer;
using Zenject;

namespace Infrastructure.Installers.Factories
{
	public class CountdownTimerFactory : IFactory<CountdownTimer>
	{
		private readonly IGameModePreferences _gameModePreferences;
		private readonly CoroutineExecutor _coroutineExecutor;

		public CountdownTimerFactory(IGameModePreferences gameModePreferences, CoroutineExecutor coroutineExecutor)
		{
			_gameModePreferences = gameModePreferences;
			_coroutineExecutor = coroutineExecutor;
		}

		public CountdownTimer Create()
		{
			return new CountdownTimer(_gameModePreferences.TimerTimeAmount, _coroutineExecutor);
		}
	}
}