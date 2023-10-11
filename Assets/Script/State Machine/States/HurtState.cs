using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HurtState : EnemyState
    {
        //[SerializeField, BoxGroup("Dialogues"), TextArea()] private string[] _hurtDialog;

        public override void EnterState(EnemyController enemy)
        {
            EnemyController = enemy;
            enemy.CurrentState = this;
            EnemyMaterial.material = _material;


        }

        public override void UpdateState(EnemyController enemy)
        {
            
            //enemy.Movement.Move(dashDir * dashSpeed * Time.fixedDeltaTime);
        }

        public override void ExitState(EnemyController enemy)
        {
            enemy.CurrentState = null;
            //if (!enemy.IsMoving) enemy.Movement.Move(Vector2.zero);

        }

        [Button]
        private void Talk() => StartCoroutine(HurtTalk());
        private IEnumerator HurtTalk()
        {
            base.EnemyTalk();
            yield return new WaitForSeconds(EnemyController.MaxTimeBetweenDialog);
            this.ExitState(EnemyController);
            EnemyController.NeutralState.EnterState(EnemyController);
        }

        
    }
}
