using CarSumo.Infrastructure.StateMachine;
using CarSumo.SceneManagement.SceneStates;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Bootstrap
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
            _stateMachine.Enter<BootstrapSceneState>();
        }
    }
}