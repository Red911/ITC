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

        [SerializeField] private float _m5P31MaxTimer = 20f;
        private float _m5P31Timer = 0;

        [SerializeField]private Animator _m5Animator;
        public override void EnterState(EnemyController enemy)
        {
            //Debug.Log("FEUR");
            enemy.CurrentState = this;
            EnemyController = enemy;
            if (!EnemyController.DialogSpawner.gameObject.activeSelf) EnemyController.DialogSpawner.SetActiveDialogSpawner(false);
            else EnemyController.DialogSpawner.SetActiveDialogSpawner(true);
            enemy.TheDialog.Ev += OnDialogFinish;

            if (enemy.enemyPhase == EnemyController.EnemyPhase.WIN)
            {
                enemy.enemyPhase = EnemyController.EnemyPhase.WIN;
                enemy.CurrentPhase = (int)EnemyController.EnemyPhase.WIN;
                if(enemy.transform.root.name == "M5")
                {
                    _m5Animator?.SetBool("Phase 3", false);
                    _m5Animator?.SetBool("Phase 2", false);
                }
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
                _dialogTalked.Clear();
                EnemyController.SetGaze();
                if (enemy.enemyPhase == EnemyController.EnemyPhase.PHASE3_PART1)
                {
                    _m5P31Timer = _m5P31MaxTimer;
                    _m5Animator?.SetBool("Phase 2", true);
                    return;
                }
                if (enemy.enemyPhase == EnemyController.EnemyPhase.PHASE4_PART1) _m5Animator?.SetBool("Phase 3", true);

            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.enemyPhase != EnemyController.EnemyPhase.PHASE3_PART1) return;
            if(_m5P31Timer <= 0)
            {
                EnemyController.HappyState.EnterState(enemy);
                this.ExitState(enemy);
            }
            else _m5P31Timer -= Time.deltaTime;
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
                EnemyController.DialogSpawner?.SetActiveDialogSpawner(false);
                EnemyController.transform.root.gameObject.SetActive(false);
                return;
            }
            if(!EnemyController.DialogSpawner.gameObject.activeSelf)EnemyController.DialogSpawner.SetActiveDialogSpawner(true);

        }

    }
}
