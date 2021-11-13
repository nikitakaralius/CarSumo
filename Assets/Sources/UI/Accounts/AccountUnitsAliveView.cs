using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using Sirenix.OdinInspector;
using TMPro;
using TweenAnimations;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
    [RequireComponent(typeof(TMP_Text))]
    public class AccountUnitsAliveView : SerializedMonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private Color _highlightedColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private ITweenAnimation _scoreAnimation;

        private TMP_Text _score;
        
        private ITeamPresenter _teamPresenter;
        private IUnitTracking _unitTracking;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [Inject]
        private void Construct(ITeamPresenter teamPresenter, IUnitTracking unitTracking)
        {
            _teamPresenter = teamPresenter;
            _unitTracking = unitTracking;
        }
        
        private void Awake()
        {
            _score = GetComponent<TMP_Text>();
            _score.text = $"{_unitTracking.UnitsAlive(_team).Count}";

            _teamPresenter.ActiveTeam
	            .Subscribe(ChangeScoreColor)
	            .AddTo(_subscriptions);

            _unitTracking
	            .UnitsAlive(_team)
                .ObserveCountChanged()
	            .Subscribe(count => _score.text = $"{count}")
	            .AddTo(_subscriptions);
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }

        private void ChangeScoreColor(Team activeTeam)
        {
            if (activeTeam == _team)
            {
                _score.color = _highlightedColor;
                _scoreAnimation.Play();
            }
            else
            {
                _score.color = _normalColor;
                _scoreAnimation.Stop();
            }
        }
    }
}