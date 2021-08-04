using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.OtherLayout
{
    [RequireComponent(typeof(Button))]
    public class BattleButton : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            GetComponent<Button>()
                .onClick
                .AddListener(() => _stateMachine.Enter<GameEntryState>());
        }
    }
}