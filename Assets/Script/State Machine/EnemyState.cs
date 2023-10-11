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

        [SerializeField, BoxGroup("Dialogues"), TextArea()] private string[] _dialog;
        private List<string> dialogTalked = new List<string>();

        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
        public abstract void ExitState(EnemyController enemy);

        //[Button("Enemy Talk", EButtonEnableMode.Playmode)]
        /*
            Lance un dialogue aléatoire
            - Ne joue jamais le même dialogue 2 fois
            - Repasse les dialogues à 0 quand toutes les dialogues sont faites
            - Attend avant de finir (pour dialog auto)
         */
        //protected void Talk() => StartCoroutine(EnemyTalk());
        protected void EnemyTalk()
        {
            if (_dialog.Length <= 0) return;
            if (dialogTalked.Count >= _dialog.Length)
            {
                dialogTalked.Clear();
                Debug.Log("CLEARED");
            }

            int randomDialogId;
            do
            {
                randomDialogId = Random.Range(0, _dialog.Length);
            }
            while (dialogTalked.Contains(_dialog[randomDialogId]));
            Debug.Log(_dialog[randomDialogId]);
            dialogTalked.Add(_dialog[randomDialogId]);
        }
    }
}
