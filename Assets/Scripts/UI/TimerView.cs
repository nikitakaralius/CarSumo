using System;
using Services.Timer;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _time;
        
        private ITimer _timer;
        private IDisposable _subscription;

        [Inject]
        private void Construct(ITimer timer)
        {
            _timer = timer;
        }

        private void Start()
        {
            _subscription = _timer.SecondsLeft
                .Subscribe(SetTimeText);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();   
        }

        private void SetTimeText(float time)
        {
            _time.text = $"{ProcessTimeValue(time)}";
        }

        private int ProcessTimeValue(float time)
        {
            return Mathf.RoundToInt(time);
        }
    }
}