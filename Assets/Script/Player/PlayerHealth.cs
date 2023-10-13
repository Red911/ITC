using Game.Script.SoundManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] int _maxHealth;
    [SerializeField] private AudioClip _hitSFX;

    public int CurrentHealth
    {
        get;
        private set;
    }
    public bool IsDead => CurrentHealth <= 0;
    public int MaxHealth { get => _maxHealth; }

    public event Action<int> OnDamage;
    //public event Action OnReset;
    public UnityEvent ResetFunction;

    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private SpriteRenderer _entitySR;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnDamage += Damaged;
        //OnDie += Death;
    }

    private void OnDisable()
    {
        OnDamage -= Damaged;
        //OnDie -= Death;
    }

    public void Damage(int amount)
    {
        Assert.IsTrue(amount >= 0);
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        Debug.Log($"PLAYER -1 HEALTH, {CurrentHealth} remaining");
        if (CurrentHealth <= 0) ResetHealth(3);
        OnDamage?.Invoke(amount);
    }
    public void ResetHealth(int amount)
    {
        Assert.IsTrue(amount >= 0);
        CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + amount);
        Debug.Log($"PLAYER RESET HEALTH, {CurrentHealth} remaining");
        ResetFunction?.Invoke();



    }

    private void Damaged(int amount)
    {
       // if (IsDead) InternalDie();
        //else StartCoroutine(DamageVFX());
    }

   /* private IEnumerator DamageVFX()
    {
        _entitySR.color = Color.red;

        if (_hitSFX != null)
        {
            ServiceLocator.Get().PlaySound(_hitSFX);
        }

        yield return new WaitForSeconds(.1f);
        _entitySR.color = Color.white;
    }*/

    /*private void Death()
    {
        //DeathParticle();
        //DeathVFX();

        if (_deathSFX != null)
        {
            ServiceLocator.Get().PlaySound(_deathSFX);
        }
    }*/

    /*private void DeathParticle()
    {
        if (_deathParticle.isPlaying) return;
        _deathParticle?.gameObject.SetActive(true);
        _deathParticle?.Play();
    }*/

    //private void DeathVFX() => _entitySR.color = Color.black;
}
