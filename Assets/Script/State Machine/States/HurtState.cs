using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HurtState : EnemyState
    {
        [SerializeField, BoxGroup("Dialogues")] private DialoguesScriptable _hurtDialog;

        public override void EnterState(EnemyController enemy)
        {
            EnemyController = enemy;
            enemy.CurrentState = this;
            enemy.TheDialog.Ev += OnDialogFinish;
            EnemyController.DialogSpawner.SetActiveDialogSpawner(false);
            enemy.TheDialog.SetDialogAndTypeSentence(_hurtDialog, Random.Range(0, _hurtDialog._dialAndSound.Length), true);
        }

        public override void UpdateState(EnemyController enemy)
        {
            
        }

        public override void ExitState(EnemyController enemy)
        {
            enemy.CurrentState = null;
            enemy.TheDialog.Ev -= OnDialogFinish;
            enemy.NeutralState.EnterState(enemy);

        }

        public override void OnDialogFinish()
        {
            EnemyController.DialogSpawner.SetActiveDialogSpawner(true);
            this.ExitState(EnemyController);
        }
    }
}
