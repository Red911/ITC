using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EnemyController : MonoBehaviour
    {

        #region States
        [SerializeField, BoxGroup("Dependencies")] EnemyStateMachine _stateMachine;

        [SerializeField] EnemyState currentState;
        public EnemyState CurrentState { get => currentState; set => currentState = value; }
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
            [Tooltip("Le nom sert juste à se reperer")]
            public string _phaseName;   
            public DialoguesScriptable _dialog;
            public GetGaze _validObject;

            public List<GetGaze> _phaseGaze;

        }
        public EnemyDial[] _enemyDial;

        [SerializeField]private int _maxPhase = 4;
        [SerializeField]private int _currentPhase;
        [SerializeField]private int _currentDialogId;

        public int MaxPhase { get => _maxPhase; }
        public int CurrentPhase { get => _currentPhase; set => _currentPhase = value; }
        public int CurrentDialogId { get => _currentDialogId; set => _currentDialogId = value; }

        [SerializeField] private List<GetGaze> allGaze;

        [SerializeField]
        private GetTheDialogue _theDialog;
        public GetTheDialogue TheDialog { get => _theDialog; }

        [SerializeField]private DialogSpawner _dialogSpawner;
        public DialogSpawner DialogSpawner { get => _dialogSpawner; set => _dialogSpawner = value; }
        public enum EnemyPhase
        {
            INTRO,
            PHASE1,
            BEFORE_PHASE2,
            PHASE2,
            BEFORE_PHASE3,
            PHASE3,
            BEFORE_PHASE4,
            PHASE4_PART1,
            BEFORE_PHASE4_PART2,
            PHASE4_PART2,
            BEFORE_PHASE4_PART3,
            PHASE4_PART3,
            WIN = 333
        }

        public EnemyPhase enemyPhase;

        [SerializeField]
        private PlayerMove _playerMove;

        [SerializeField]
        private Transform _endPoint;

        [SerializeField, InfoBox("LANCE EVENT QUAND LE MONSTRE MEURT"), BoxGroup("EVENT")]
        private UnityEvent _event;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.CurrentEnemy = this;
            currentState = NeutralState;
            currentState.EnterState(this);
            DialogSpawner.enemy = this;

            _timeBetweenDialog = _maxTimeBetweenDialog;
        }

        public void ResetEnemy()
        {
            enemyPhase = EnemyPhase.PHASE1;
            _currentPhase = 1;
            _neutralState.ResetTalkedDialog();

            SetGaze();

        }

        public void SetGaze()
        {
            ReplaceGazeWithNewGaze();
            ChangeGazeValid(_enemyDial[CurrentPhase]._validObject);
        }

        private void ChangeGazeValid(GetGaze validGaze)
        {
            foreach (GetGaze gaze in allGaze)
            {
                if (gaze == validGaze)
                    validGaze._type = GetGaze.GazeType.VALID;
                else gaze._type = GetGaze.GazeType.INVALID;
            }

        }

        private void ReplaceGazeWithNewGaze()
        {
            if (_enemyDial[_currentPhase]._phaseGaze.Count < 1) return;
            foreach(GetGaze ancientGaze in allGaze)ancientGaze.gameObject.SetActive(false);
            allGaze.Clear();
            foreach (GetGaze newGaze in _enemyDial[_currentPhase]._phaseGaze)
            {
                newGaze.gameObject.SetActive(true);
                allGaze.Add(newGaze);
            }


        }

        private void OnDisable()
        {
            _playerMove.EndingPoint = _endPoint;
            _playerMove.Move();
            _event.Invoke();
        }

    }
}
