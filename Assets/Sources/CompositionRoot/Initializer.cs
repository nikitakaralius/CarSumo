using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CompositionRoot
{
    public class Initializer : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _stateMachine.Enter<BootstrapState>();
        }
    }
}