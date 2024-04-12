using UnityEngine;
using TMPro;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public TextMeshProUGUI Health;
    public Transform Player;
    public AudioSource HitSound;
    public int CurrentHealth = 100;
    public int MaxHealth = 100;
    public float InvincibilityTime = 1.0f;
    public bool Invincibility = false;
    public float InvincibilityTimer;
    public GameObject DeathMenuUI;
    public FirstPersonController FirstPersonController;
    private bool _isDead = false;
    [HideInInspector]
    public bool Dead;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CheckHealth();

        if (_isDead)
        {
            DeathMenuUI.SetActive(true);
            FirstPersonController.enabled = false;
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
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        Invincibility = true;
        InvincibilityTimer = Time.time + InvincibilityTime;
        Health.text = "Health: " + CurrentHealth;
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