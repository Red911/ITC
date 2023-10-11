using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyStateMachine : MonoBehaviour
    {
        EnemyController _enemyController;

        void Start()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        public void SwitchState(EnemyState state)
        {
            //if (playerController.currentState != null) return;

            _enemyController.CurrentState.ExitState(_enemyController);
            _enemyController.CurrentState = state;
            state.EnterState(_enemyController);
        }
    }
}
