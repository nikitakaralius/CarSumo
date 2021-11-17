using Services.Timer.InGameTimer;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _time;
        
        private ITimer _timer;
        private readonly CompositeDisposable _disposables = new CompositeDisposable(1);

        [Inject]
        private void Construct(ITimer timer) => _timer = timer;

        private void Start() =>
            _timer.SecondsLeft
                .Subscribe(ChangeTimeText)
                .AddTo(_disposables);

        private void OnDestroy() => _disposables.Dispose();

        private void ChangeTimeText(float time) => _time.text = $"{RoundTime(time)}";

        private static int RoundTime(float value) => Mathf.RoundToInt(value);
    }
}