using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePopup;

        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(EnterPauseState);
        }
        
        public void EnterPauseState()
        {
            _pausePopup.SetActive(true);
            _stateMachine.Enter<PauseState>();
        }

        public void ExitPauseState()
        {
            _pausePopup.SetActive(false);
            _stateMachine.Enter<GameState>();
        }
    }
}