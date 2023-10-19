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
            EnemyMaterial.material = _material;
            enemy.TheDialog.SetDialogAndTypeSentence(_hurtDialog, Random.Range(0, _hurtDialog.dialogs.Length), true);
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
