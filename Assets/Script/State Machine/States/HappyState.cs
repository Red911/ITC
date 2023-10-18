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
            //EnemyMaterial.material = _material;
            enemy.TheDialog.Ev += OnDialogFinish;
            enemy.DialogSpawner.SetActiveSpawner(false);

            switch(enemy.enemyPhase)
            {
                case EnemyController.EnemyPhase.PHASE1:
                    EnemyController.CurrentPhase = (int)EnemyController.enemyPhase + 1;
                    EnemyController.enemyPhase = EnemyController.enemyPhase + 1;
                    enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog1, 0);

                break;
                case EnemyController.EnemyPhase.PHASE2:
                    EnemyController.CurrentPhase = (int)EnemyController.enemyPhase + 1;
                    EnemyController.enemyPhase = EnemyController.enemyPhase + 1;
                    if (enemy.CurrentPhase >= enemy.MaxPhase)
                    {
                        enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                        this.ExitState(EnemyController);
                        EnemyController.NeutralState.EnterState(EnemyController);
                    }
                    else enemy.TheDialog.SetDialogAndTypeSentence(_happyDialog2, 0);
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
            if (!EnemyController.DialogSpawner.gameObject.activeSelf) EnemyController.DialogSpawner.SetActiveSpawner(true);
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }


    }
}
