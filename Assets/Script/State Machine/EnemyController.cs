using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class EnemyController : MonoBehaviour
    {
        //[SerializeField, BoxGroup("Dependencies")] EntityMovement _movement;

        #region States
        [SerializeField, BoxGroup("Dependencies")] EnemyStateMachine _stateMachine;

        [SerializeField] EnemyState currentState;
        public EnemyState CurrentState { get => currentState; set => currentState = value; }
        //public EntityMovement Movement { get => _movement; set => _movement = value; }
        public EnemyStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }
        public NeutralState NeutralState { get => _neutralState; set => _neutralState = value; }
        public HurtState HurtState { get => _dashState; set => _dashState = value; }
        public HappyState HappyState { get => _happyState; set => _happyState = value; }

        [SerializeField, BoxGroup("States")] NeutralState _neutralState;
        [SerializeField, BoxGroup("States")] HurtState _dashState;
        [SerializeField, BoxGroup("States")] HappyState _happyState;

        [SerializeField] private float _maxTimeBetweenDialog = 3f;
        public float MaxTimeBetweenDialog { get => _maxTimeBetweenDialog; }
        [SerializeField] private float _timeBetweenDialog;

        private Player player;
        #endregion
        [System.Serializable]
        public struct EnemyDial
        {
            //[Tooltip("Le nom sert juste à se reperer")]
            public string _phaseName;   
            public DialoguesScriptable _dialog;
            public GetGaze _validObject;

        }
        public EnemyDial[] _enemyDial;

        [SerializeField]private int _maxPhase = 2;
        [SerializeField]private int _currentPhase;

        public int MaxPhase { get => _maxPhase; }
        public int CurrentPhase { get => _currentPhase; set => _currentPhase = value; }



        public enum EnemyPhase
        {
            INTRO,
            PHASE1,
            BEFOREPHASE2,
            PHASE2,
            BEFOREPHASE3,
            PHASE3,
            PHASE4_PART1,
            PHASE4_PART2,
            PHASE4_PART3,
            WIN
        }

        public EnemyPhase enemyPhase;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.CurrentEnemy = this;
            currentState = NeutralState;
            currentState.EnterState(this);

            _timeBetweenDialog = _maxTimeBetweenDialog;
        }

        public void ResetEnemy()
        {
            enemyPhase = EnemyPhase.PHASE1;
            _neutralState.ResetTalkedDialog();
        }

    }
}
