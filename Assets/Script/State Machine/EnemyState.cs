using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game
{
    public abstract class EnemyState : MonoBehaviour
    {
        private EnemyController _enemyController;

        public EnemyController EnemyController { get => _enemyController; set => _enemyController = value; }

        [SerializeField]
        protected Material _material;

        [SerializeField] private MeshRenderer _enemyMaterial;
        public MeshRenderer EnemyMaterial { get => _enemyMaterial; set => _enemyMaterial = value; }

        //[SerializeField] public DialoguesScriptable _dialog;

        protected List<string> _dialogTalked = new List<string>();

        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
        public abstract void ExitState(EnemyController enemy);


        public abstract void OnDialogFinish();

        public void ResetTalkedDialog() => _dialogTalked.Clear();
            


    }
}
