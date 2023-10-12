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

        [SerializeField] protected DialoguesScriptable _dialog;
        protected List<string> _dialogTalked = new List<string>();

        [SerializeField]
        protected GetTheDialogue theDialogue;

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
        protected void EnemyTalk(bool randomTalk, int talkId = 0)
        {
            if (_dialog.dialogs.Length <= 0) return;
            if (_dialogTalked.Count >= _dialog.dialogs.Length)
            {
                _dialogTalked.Clear();
                Debug.Log("CLEARED");
            }

            int randomDialogId;
            if (randomTalk)
            {
                do
                {
                    randomDialogId = Random.Range(0, _dialog.dialogs.Length);
                }
                while (_dialogTalked.Contains(_dialog.dialogs[randomDialogId]));
                Debug.Log(_dialog.dialogs[randomDialogId]);
                _dialogTalked.Add(_dialog.dialogs[randomDialogId]);
            }
            else Debug.Log(_dialog.dialogs[talkId]);
        }

        protected void EnemyTalk(DialoguesScriptable dialog, int id)
        {
            Debug.Log(dialog.dialogs[id]);
        }

        public void AddDialogOnTalkedDialog(string text)
        {
            _dialogTalked.Add(text);
        }

        public abstract void OnDialogFinish();

        /*public int CheckIdAlreadyTalked()
        {
            int randomDialogId;
            do
            {
                randomDialogId = Random.Range(0, _dialog.dialogs.Length);
            }
            while (_dialogTalked.Contains(_dialog.dialogs[randomDialogId]));
            Debug.Log(_dialog.dialogs[randomDialogId]);
            _dialogTalked.Add(_dialog.dialogs[randomDialogId]);
            return randomDialogId;
        }*/


    }
}
