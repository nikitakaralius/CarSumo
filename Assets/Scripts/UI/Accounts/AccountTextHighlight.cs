using System;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using Sirenix.OdinInspector;
using TMPro;
using TweenAnimations;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
    [RequireComponent(typeof(TMP_Text))]
    public class AccountTextHighlight : SerializedMonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private Color _highlightedColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private ITweenAnimation _scoreAnimation;

        private TMP_Text _score;
        private ITeamPresenter _teamPresenter;
        private IDisposable _subscription;

        [Inject]
        private void Construct(ITeamPresenter teamPresenter)
        {
            _teamPresenter = teamPresenter;
        }

        private void Start()
        {
            _score = GetComponent<TMP_Text>();
            _subscription = _teamPresenter.ActiveTeam.Subscribe(ChangeScoreColor);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
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