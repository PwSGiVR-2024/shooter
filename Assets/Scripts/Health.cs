using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _startedHealth = 100;
    protected int _currentHealth;

    public int MaxHealth => _maxHealth;

    public virtual int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    protected virtual void Start()
    {
        CurrentHealth = _startedHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}