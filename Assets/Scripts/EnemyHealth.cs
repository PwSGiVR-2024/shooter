using System;

public class EnemyHealth : Health
{
    public event Action OnEnemyDeath;
    public event Action OnEnemyTakeDamage;

    protected override void Start()
    {
        base.Start();
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
    }
}