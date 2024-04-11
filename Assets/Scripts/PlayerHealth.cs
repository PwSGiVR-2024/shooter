using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource HitSound;
    public Transform Player;
    public int CurrentHealth = 3;
    public int MaxHealth = 3;
    public float InvincibilityTime = 2f;
    public bool Invincibility = false;
    public float InvincibilityTimer;

    void Start()
    {
        CurrentHealth = MaxHealth; 
    }

    void Update()
    {
        CheckHealth();
        if (Invincibility)
        {
            if (Time.time > InvincibilityTimer)
            {
                Invincibility = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void CheckHealth()
    {
        if (CurrentHealth <= 0 && !DeathManager.Instance.Dead)
        {
            DeathManager.Instance.YouDied();
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Invincibility = true;
        InvincibilityTimer = Time.time + InvincibilityTime;
    }
}