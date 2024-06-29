using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public TextMeshProUGUI Health;
    public Transform PlayerTransform;
    public AudioSource HitSound;
    private int _currentHealth = 100;
    public int MaxHealth = 100;
    public float InvincibilityTime = 1.0f;
    public bool Invincibility = false;
    public float InvincibilityTimer;
    public GameObject DeathMenuUI;
    public FirstPersonController FirstPersonController;
    public GameObject shootingManager;
    private List<MonoBehaviour> shootingScripts = new List<MonoBehaviour>();
    private bool _isDead = false;
    [HideInInspector]
    public bool Dead;
    [HideInInspector]
    public int ProtectionPercentage = 0;

    private void Awake()
    {
        Instance = this;

        foreach (MonoBehaviour script in shootingManager.GetComponents<MonoBehaviour>())
        {
            shootingScripts.Add(script);
        }
    }
    
    void Update()
    {
        if (_isDead)
        {
            DeathMenuUI.SetActive(true);
            FirstPersonController.enabled = false;

            foreach (MonoBehaviour script in shootingScripts)
            {
                script.enabled = false;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isDead = false;
        }

        if (Invincibility)
        {
            if (Time.time > InvincibilityTimer)
            {
                Invincibility = false;
            }
        }
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            int damage = _currentHealth - value; // Calculate the incoming damage
            if (damage > 0)
            {
                // Apply protection to reduce the damage
                int effectiveDamage = damage - (damage * ProtectionPercentage / 100);
                _currentHealth -= effectiveDamage; // Subtract the reduced damage from current health
            }
            else
            {
                // If it's not damage (e.g., healing), just update the health directly
                _currentHealth = value;
            }
            CheckHealth();
            Health.text = "Health: " + _currentHealth;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            TakeDamage(33);
        }
    }

    public void CheckHealth()
    {
        if (CurrentHealth <= 0 && !Dead)
        {
            YouDied();
        }
        else if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        Invincibility = true;
        InvincibilityTimer = Time.time + InvincibilityTime;
    }

    public void YouDied()
    {
        if (!Dead)
        {
            Dead = true;
            _isDead = true;
        }
    }
}