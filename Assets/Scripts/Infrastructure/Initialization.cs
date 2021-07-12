using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure
{
    public class Initialization : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.Enter<GameEntryState>();
        }
    }
}