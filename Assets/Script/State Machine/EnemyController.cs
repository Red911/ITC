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
        [SerializeField, BoxGroup("Dependencies")] Transform _spriteTransform;
        [SerializeField, BoxGroup("Dependencies")] Transform _cursor;
        public Transform SpriteTransform { get => _spriteTransform; set => _spriteTransform = value; }
        public Transform Cursor { get => _cursor; set => _cursor = value; }

        #region States
        [SerializeField, BoxGroup("Dependencies")] EnemyStateMachine _stateMachine;

        [SerializeField]EnemyState currentState;
        public EnemyState CurrentState { get => currentState; set => currentState = value; }
        //public EntityMovement Movement { get => _movement; set => _movement = value; }
        public EnemyStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }
        public NeutralState NeutralState { get => _neutralState; set => _neutralState = value; }
        public HurtState HurtState { get => _dashState; set => _dashState = value; }
        public HappyState HappyState { get => _happyState; set => _happyState = value; }

        [SerializeField, BoxGroup("States")] NeutralState _neutralState;
        [SerializeField, BoxGroup("States")] HurtState _dashState;
        [SerializeField, BoxGroup("States")] HappyState _happyState;

        [SerializeField]private float _maxTimeBetweenDialog = 3f;
        public float MaxTimeBetweenDialog { get => _maxTimeBetweenDialog; }
        [SerializeField]private float _timeBetweenDialog;

        #endregion

        bool isMoving;
        public bool IsMoving { get => isMoving; set => isMoving = value; }
        Vector2 move;
        public Vector2 Move { get => move; set => move = value; }

        Vector2 moveDir { get; set; }

        bool canDash;
        float dashCD;
        [SerializeField] float dashCDMax;
        public bool CanDash { get => canDash; set => canDash = value; }
        public float DashCD { get => dashCD; set => dashCD = value; }
        public float DashCDMax { get => dashCDMax; set => dashCDMax = value; }

        bool facingRight;

        public enum Phase
        {
            INTRO,
            PHASE1,
            INTERMEDIAIRE,
            PHASE2,
            WIN
        }

        public Phase enemyPhase;

        private void Start()
        {
            currentState = NeutralState;
            currentState.EnterState(this);

            canDash = true;
            facingRight = true;
            _timeBetweenDialog = _maxTimeBetweenDialog;
        }

        private void FixedUpdate()
        {
            currentState.UpdateState(this);

            if (dashCD > 0) dashCD = Mathf.Clamp(dashCD - Time.deltaTime, 0, dashCDMax);
            else if (!canDash) canDash = true;

            if (move.x > 0 && !facingRight || move.x < 0 && facingRight) Flip();
        }

        public void SetMoveDir(Vector2 mDir) => moveDir = mDir;

        void Flip()
        {
            facingRight = !facingRight;

            _spriteTransform.Rotate(0f, 180f, 0f);
        }
    }
}
