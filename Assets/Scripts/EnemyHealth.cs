using System;

public class EnemyHealth : Health
{
    public event Action OnEnemyDeath;

    protected override void Die()
    {
        OnEnemyDeath?.Invoke();
    }
}