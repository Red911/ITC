using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class NeutralState : EnemyState  
    {
        
        public enum Phase
        {
            INTRO,
            PHASE1,
            INTERMEDIAIRE,
            PHASE2
        }

        public Phase enemyPhase;
        public override void EnterState(EnemyController enemy)
        {
            enemy.CurrentState = this;
            EnemyController = enemy;
            EnemyMaterial.material = _material;
            //enemy.Movement.CanMove = true;
        }

        public override void UpdateState(EnemyController enemy)
        {
            

            //enemy.Movement.Move(theMove);
        }

        public override void ExitState(EnemyController enemy)
        {
            enemy.CurrentState = null;
            //enemy.Movement.CanMove = false;
        }

        [Button]
        private void EnterHurtState() => EnemyController.HurtState.EnterState(EnemyController);
        
        [Button]
        private void EnterHappyState() => EnemyController.HappyState.EnterState(EnemyController);

        [Button]
        private void NeutralTalk() => base.EnemyTalk(true);


    }
}
