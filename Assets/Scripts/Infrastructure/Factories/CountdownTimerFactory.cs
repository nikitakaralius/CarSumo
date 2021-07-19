using CarSumo.Coroutines;
using CarSumo.Infrastructure.Services.TimerService;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class CountdownTimerFactory : IFactory<ITimerService>
    {
        private const int StartTimeRemaining = 10;
        private readonly CoroutineExecutor _coroutineExecutor;

        public CountdownTimerFactory(CoroutineExecutor coroutineExecutor)
        {
            _coroutineExecutor = coroutineExecutor;
        }

        public ITimerService Create()
        {
            return new CountdownTimer(StartTimeRemaining, _coroutineExecutor);
        }
    }
}