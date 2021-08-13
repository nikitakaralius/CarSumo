using System;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using GameModes;
using Sirenix.OdinInspector;
using TMPro;
using TweenAnimations;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
    [RequireComponent(typeof(TMP_Text))]
    public class AccountUnitAliveView : SerializedMonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private Color _highlightedColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private ITweenAnimation _scoreAnimation;

        private TMP_Text _score;
        private ITeamPresenter _teamPresenter;
        private IUnitTracker _unitTracker;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [Inject]
        private void Construct(ITeamPresenter teamPresenter, IUnitTracker unitTracker)
        {
            _teamPresenter = teamPresenter;
            _unitTracker = unitTracker;
        }

        private void Start()
        {
            _score = GetComponent<TMP_Text>();
            
            _teamPresenter.ActiveTeam
	            .Subscribe(ChangeScoreColor)
	            .AddTo(_subscriptions);

            _unitTracker
	            .GetUnitsAlive(_team)
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