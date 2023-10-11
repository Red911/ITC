using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HappyState : EnemyState
    {
        //[SerializeField, BoxGroup("Dialogues"), TextArea()] private string[] _happyDialog;

        public override void EnterState(EnemyController enemy)
        {
            //base.EnterState(enemy);
            EnemyController = enemy;
            enemy.CurrentState = this;
            EnemyMaterial.material = _material;
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



    }
}
