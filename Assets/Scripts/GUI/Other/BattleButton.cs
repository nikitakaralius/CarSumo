using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CarSumo.GUI.Other
{
    public class BattleButton : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnButtonClicked()
        {
            _stateMachine.Enter<GameEntryState>();
        }
    }
}