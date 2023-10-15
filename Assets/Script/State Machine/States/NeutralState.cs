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

        [SerializeField] AudioClip sound;
        public override void EnterState(EnemyController enemy)
        {
            enemy.CurrentState = this;
            EnemyController = enemy;
            EnemyMaterial.material = _material;
            switch(enemy.enemyPhase)
            {
                case EnemyController.Phase.INTRO:
                    theDialogue.SetDialogAndTypeSentence(introDialog, 0, sound);
                    //StartCoroutine(NeutralTalk(introDialog));
                    enemy.enemyPhase = EnemyController.Phase.PHASE1;
                    _dialogTalked.Clear();
                    
                break;

                case EnemyController.Phase.INTERMEDIAIRE:
                    //StartCoroutine(NeutralTalk(intermediaireDialog));
                    theDialogue.SetDialogAndTypeSentence(intermediaireDialog, 0, sound);
                    enemy.enemyPhase = EnemyController.Phase.PHASE2;
                    _dialogTalked.Clear();
                break;
                case EnemyController.Phase.WIN:
                    Debug.Log("WINNNN");
                   EnemyController.gameObject.SetActive(false);
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

        public void EnterHurtState() => EnemyController.HurtState.EnterState(EnemyController);
        
        public void EnterHappyState() => EnemyController.HappyState.EnterState(EnemyController);

        [Button]
        public void TalkNeutral() => theDialogue.SetDialogAndTypeSentence(_dialog, Random.Range(0, _dialog.dialogs.Length), true);

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

        public override void OnDialogFinish()
        {
            return;
        }

    }
}
