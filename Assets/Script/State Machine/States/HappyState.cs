using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Game
{
    public class HappyState : EnemyState
    {
        [SerializeField, BoxGroup("Dialogues")] private DialoguesScriptable _happyDialog1;
        [SerializeField, BoxGroup("Dialogues")] private DialoguesScriptable _happyDialog2;

        public override void EnterState(EnemyController enemy)
        {
            EnemyController = enemy;
            enemy.CurrentState = this;
            EnemyMaterial.material = _material;
            enemy.TheDialog.Ev += OnDialogFinish;

            switch(enemy.enemyPhase)
            {
                case EnemyController.EnemyPhase.PHASE1:
                    enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog1, 0);

                    enemy.enemyPhase = EnemyController.EnemyPhase.BEFORE_PHASE2;
                break;
                case EnemyController.EnemyPhase.PHASE2:
                    enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog2, 0);
                    enemy.enemyPhase = EnemyController.EnemyPhase.WIN;

                break;
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            

        }

        public override void ExitState(EnemyController enemy)
        {
            enemy.CurrentState = null;
            enemy.TheDialog.Ev -= OnDialogFinish;
        }
       

        public override void OnDialogFinish()
        {
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }


    }
}
