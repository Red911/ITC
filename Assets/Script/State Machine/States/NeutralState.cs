using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class NeutralState : EnemyState  
    {
        
        
        [SerializeField]
        private DialoguesScriptable introDialog;
        
        [SerializeField]
        private DialoguesScriptable intermediaireDialog;
        public override void EnterState(EnemyController enemy)
        {
            enemy.CurrentState = this;
            EnemyController = enemy;
            EnemyMaterial.material = _material;
            switch(enemy.enemyPhase)
            {
                case EnemyController.Phase.INTRO:
                    StartCoroutine(NeutralTalk(introDialog));
                    enemy.enemyPhase = EnemyController.Phase.PHASE1;
                break;

                case EnemyController.Phase.INTERMEDIAIRE:
                    StartCoroutine(NeutralTalk(intermediaireDialog));
                    enemy.enemyPhase = EnemyController.Phase.PHASE2;
                break;
            }
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
        private void TalkNeutral() => base.EnemyTalk(true);

        private IEnumerator NeutralTalk(DialoguesScriptable dialog = null)
        {
            if(dialog != null)
            {
                for (int i = 0; i < dialog.dialogs.Length; i++)
                {
                    base.EnemyTalk(dialog, i);
                    yield return new WaitForSeconds(EnemyController.MaxTimeBetweenDialog);
                }
                TalkNeutral();
            }
            else base.EnemyTalk(true);

            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }

    }
}
