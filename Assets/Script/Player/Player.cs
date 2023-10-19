using Game;
using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private PlayerHealth _health;
    public PlayerHealth Health { get => _health; set => _health = value; }
    [SerializeField]private EnemyController _currentEnemy;
    public EnemyController CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }

    [SerializeField]private bool _isTalking;
    public bool IsTalking { get => _isTalking; set => _isTalking = value; }

    [SerializeField] private AudioClip _ambiance;

    private void Start()
    {
        ServiceLocator.Get().PlaySound(_ambiance);
    }


    public void DamagePlayer(int damage)
    {
        _health.Damage(damage);
    }

    public void ResetEnemy() => _currentEnemy.ResetEnemy();
}
