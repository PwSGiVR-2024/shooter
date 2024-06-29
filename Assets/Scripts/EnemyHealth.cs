using System;

public class EnemyHealth : Health
{
    public event Action OnEnemyDeath;

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
        base.TakeDamage(damage);
    }
}