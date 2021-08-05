using UnityEngine;

namespace CarSumo.StateMachine.States
{
    public class GameState : IState
    {
        public void Enter()
        {
            Debug.Log("Game state has entered");
        }

        public void Exit()
        {
            
        }
    }
}