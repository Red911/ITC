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
            Debug.Log("FEUR");
            enemy.CurrentState = this;
            EnemyController = enemy;
            enemy.TheDialog.Ev += OnDialogFinish;

            if(enemy.enemyPhase == EnemyController.EnemyPhase.WIN)
            {
                enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                enemy.CurrentPhase = (int)EnemyController.EnemyPhase.WIN;
                enemy.TheDialog.SetDialogAndTypeSentence(enemy._enemyDial[enemy._enemyDial.Length - 1]._dialog, 0);
            }
            else if((int)enemy.enemyPhase % 2 == 0)
            {
                //avant chaque phase principale
                enemy.CurrentPhase = (int)enemy.enemyPhase;
                enemy.TheDialog.SetDialogAndTypeSentence(enemy._enemyDial[enemy.CurrentPhase]._dialog, 0);
                if (EnemyController._enemyDial[EnemyController.CurrentPhase]._validObject != null) EnemyController._enemyDial[EnemyController.CurrentPhase]._validObject._type = GetGaze.GazeType.VALID;
                EnemyController.CurrentPhase = (int)EnemyController.enemyPhase + 1;
                EnemyController.enemyPhase = EnemyController.enemyPhase + 1;
                EnemyController.SetGaze();
                _dialogTalked.Clear();
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

        public void EnterHurtState() => EnemyController.HurtState.EnterState(EnemyController);
        
        public void EnterHappyState() => EnemyController.HappyState.EnterState(EnemyController);

        [Button]
        public void TalkNeutral() => EnemyController.TheDialog.SetDialogAndTypeSentence(EnemyController._enemyDial[EnemyController.CurrentPhase]._dialog, Random.Range(0, EnemyController._enemyDial[EnemyController.CurrentPhase]._dialog._dialAndSound.Length), true);

        public override void OnDialogFinish()
        {
            if (EnemyController.enemyPhase == EnemyController.EnemyPhase.WIN)
            {
                EnemyController.transform.root.gameObject.SetActive(false);
                return;
            }
            if(!EnemyController.DialogSpawner.gameObject.activeSelf)EnemyController.DialogSpawner.SetActiveDialogSpawner(true);

        }

    }
}
