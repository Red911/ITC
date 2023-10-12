using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Game
{
    public class HappyState : EnemyState
    {
        //[SerializeField, BoxGroup("Dialogues"), TextArea()] private string[] _happyDialog;
        [SerializeField]private DialoguesScriptable _winDialog;

        public override void EnterState(EnemyController enemy)
        {
            //base.EnterState(enemy);
            EnemyController = enemy;
            enemy.CurrentState = this;
            EnemyMaterial.material = _material;
            theDialogue.Ev += OnDialogFinish;

            switch(enemy.enemyPhase)
            {
                case EnemyController.Phase.PHASE1:
                    //StartCoroutine(HappyTalk(_dialog));
                    theDialogue.SetDialogAndTypeSentence(_dialog, 0);

                    enemy.enemyPhase = EnemyController.Phase.INTERMEDIAIRE;
                break;
                case EnemyController.Phase.PHASE2:
                    //StartCoroutine(HappyTalk(_winDialog));
                    theDialogue.SetDialogAndTypeSentence(_winDialog, 0);
                    enemy.enemyPhase = EnemyController.Phase.WIN;
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
            theDialogue.Ev -= OnDialogFinish;
            //enemy.Movement.CanMove = false;
        }
        private IEnumerator HappyTalk(DialoguesScriptable dialog)
        {
            for(int i = 0; i < _dialog.dialogs.Length; i++)
            {
                base.EnemyTalk(dialog, i);
                yield return new WaitForSeconds(EnemyController.MaxTimeBetweenDialog);
            }
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }

        public override void OnDialogFinish()
        {
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }

        /*[Button]
        private void Talk() => StartCoroutine(HappyTalk());*/

    }
}
