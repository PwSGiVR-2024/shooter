using System;
using StarterAssets;
using TMPro;
using UnityEngine;

public class PlayerHealth : Health
{
    public Action OnPlayerDie;

    public float InvincibilityTimer;
    public float InvincibilityTime = 1.0f;
    public bool Invincibility = false;
    public int ProtectionPercentage = 0;

    [SerializeField] private GameObject _deathMenuUI;
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private GameObject _shootingManager;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _damageSound;

    private AudioSource _audioSource;
    private bool _isDead = false;

    protected override void Start()
    {
        base.Start();
        UIController.Instance.UpdateHealthUI(CurrentHealth);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Invincibility)
        {
            if (Time.time > InvincibilityTimer)
            {
                Invincibility = false;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Invincibility = true;
        InvincibilityTimer = Time.time + InvincibilityTime;
        BloodEffectManager.Instance.ShowBloodEffect();

        if (!_isDead)
        {
            PlaySound(_damageSound);
        }
    }

    protected override void Die()
    {
        if (_isDead)
        {
            return;
        }

        PlaySound(_deathSound);
        _isDead = true;
        _deathMenuUI.SetActive(true);
        _firstPersonController.enabled = false;
        DisableShootingScripts();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableShootingScripts()
    {
        _shootingManager.SetActive(false);
    }

    public override int CurrentHealth
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
            _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
            UIController.Instance.UpdateHealthUI(_currentHealth);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}