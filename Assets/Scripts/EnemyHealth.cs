using System;
using UnityEngine;

public class EnemyHealth : Health
{
    public event Action OnEnemyDeath;
    public event Action OnEnemyTakeDamage;

    private AudioSource _audioSource;

    protected override void Start()
    {
        base.Start();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override void Die()
    {
        OnEnemyDeath?.Invoke();
    }

    public override void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }
        OnEnemyTakeDamage?.Invoke();
        base.TakeDamage(damage);
        _audioSource.Play();
    }
}