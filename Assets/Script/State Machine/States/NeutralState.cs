using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

namespace Game
{
    public class NeutralState : EnemyState  
    {

        [SerializeField] AudioClip sound;
        public override void EnterState(EnemyController enemy)
        {
            enemy.CurrentState = this;
            EnemyController = enemy;
            EnemyMaterial.material = _material;

            if(enemy.enemyPhase == EnemyController.EnemyPhase.WIN)
            {
                enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                enemy.CurrentPhase = (int)EnemyController.EnemyPhase.WIN;
                enemy.TheDialog.SetDialogAndTypeSentence(enemy._enemyDial[enemy._enemyDial.Length - 1]._dialog, 0);
                enemy.TheDialog.Ev += OnDialogFinish;
            }
            else if((int)enemy.enemyPhase % 2 == 0)
            {
                //avant chaque phase principale
                enemy.CurrentPhase = (int)enemy.enemyPhase;
                enemy.TheDialog.SetDialogAndTypeSentence(enemy._enemyDial[enemy.CurrentPhase]._dialog, 0);
                enemy.CurrentPhase = (int)enemy.enemyPhase +1;
                enemy.enemyPhase = enemy.enemyPhase + 1;
                enemy.MakeAllGazeFalse();
                enemy._enemyDial[enemy.CurrentPhase]._validObject._type = GetGaze.GazeType.VALID;
                _dialogTalked.Clear();
            }
        }

        public override void UpdateState(EnemyController enemy)
        {

        }

        public override void ExitState(EnemyController enemy)
        {
            enemy.CurrentState = null;
            if(enemy.enemyPhase == EnemyController.EnemyPhase.WIN)enemy.TheDialog.Ev -= OnDialogFinish;
        }

        public void EnterHurtState() => EnemyController.HurtState.EnterState(EnemyController);
        
        public void EnterHappyState() => EnemyController.HappyState.EnterState(EnemyController);

        [Button]
        public void TalkNeutral() => EnemyController.TheDialog.SetDialogAndTypeSentence(EnemyController._enemyDial[EnemyController.CurrentPhase]._dialog, Random.Range(0, EnemyController._enemyDial[EnemyController.CurrentPhase]._dialog.dialogs.Length), true);

        public override void OnDialogFinish()
        {
            if (EnemyController.enemyPhase != EnemyController.EnemyPhase.WIN) return;
            EnemyController.transform.root.gameObject.SetActive(false);
        }

    }
}
