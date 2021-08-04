﻿using Services.Timer;
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

        [Inject]
        private void Construct(ITimer timer)
        {
            _timer = timer;
        }

        private void Start()
        {
            _timer.SecondsLeft
                .Subscribe(time => SetTimeText((int)time));
        }

        private void SetTimeText(int time)
        {
            _time.text = $"{time}";
        }
    }
}