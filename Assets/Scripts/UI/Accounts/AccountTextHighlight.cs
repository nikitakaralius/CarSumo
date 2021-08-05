using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
    [RequireComponent(typeof(TMP_Text))]
    public class AccountTextHighlight : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private Color _highlightedColor;
        [SerializeField] private Color _normalColor;

        private TMP_Text _score;
        private ITeamPresenter _teamPresenter;
        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

        [Inject]
        private void Construct(ITeamPresenter teamPresenter)
        {
            _teamPresenter = teamPresenter;
        }

        private void Start()
        {
            _score = GetComponent<TMP_Text>();
            _teamPresenter.ActiveTeam.Subscribe(ChangeScoreColor);
        }

        private void ChangeScoreColor(Team activeTeam)
        {
            if (activeTeam == _team)
            {
                _score.color = _highlightedColor;
                _tween = _score.rectTransform
                    .DOScale(Vector3.one * 1.2f, 0.7f)
                    .SetEase(Ease.InOutBack)
                    .SetLoops(-1, LoopType.Yoyo)
                    .OnKill(() => _score.rectTransform.localScale = Vector3.one);
                return;
            }

            _score.color = _normalColor;
            _tween?.Kill();
        }
    }
}