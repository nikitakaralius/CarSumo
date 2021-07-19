using CarSumo.Infrastructure.Services.TimerService;
using TMPro;
using UnityEngine;
using Zenject;

namespace CarSumo.GUI.Other
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timer;
        
        private ITimerService _timerService;

        [Inject]
        private void Construct(ITimerService timerService)
        {
            _timerService = timerService;
        }

        private void Update()
        {
            _timer.text = $"{(int)_timerService.Seconds}";
        }
    }
}