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

        public override void EnterState(EnemyController enemy)
        {
            EnemyController = enemy;
            enemy.CurrentState = this;
            enemy.TheDialog.Ev += OnDialogFinish;
            EnemyController.DialogSpawner.SetActiveDialogSpawner(false);

            if(enemy.CurrentPhase >= enemy.MaxPhase - 1)
            {
                enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                this.ExitState(EnemyController);
                EnemyController.NeutralState.EnterState(EnemyController);
            }
            EnemyController.CurrentPhase = (int)EnemyController.enemyPhase + 1;
            EnemyController.enemyPhase = EnemyController.enemyPhase + 1;
            enemy.TheDialog.SetDialogAndTypeSentence(EnemyController._enemyDial[EnemyController.CurrentPhase]._dialog, 0);


            /*switch (enemy.enemyPhase)
            {
                case EnemyController.EnemyPhase.PHASE1:


                break;
                case EnemyController.EnemyPhase.PHASE2:
                    if (enemy.CurrentPhase >= enemy.MaxPhase - 1)
                    {
                        
                        break;
                    }

                    enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog2, 0);

                break;

                case EnemyController.EnemyPhase.PHASE3:
                    if (enemy.CurrentPhase >= enemy.MaxPhase - 1)
                    {
                        enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                        this.ExitState(EnemyController);
                        EnemyController.NeutralState.EnterState(EnemyController);
                        break;
                    }

                    enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog3, 0);

                    break;
            }
            if (enemy.enemyPhase == EnemyController.EnemyPhase.WIN) return;*/
            

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

            EnemyController.DialogSpawner.SetActiveDialogSpawner(true);
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }


    }
}
