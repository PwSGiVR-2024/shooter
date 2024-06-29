using StarterAssets;
using TMPro;
using UnityEngine;

public class PlayerHealth : Health
{
    public TextMeshProUGUI HealthText;
    public GameObject DeathMenuUI;
    public FirstPersonController FirstPersonController;
    public GameObject ShootingManager;

    protected override void Start()
    {
        base.Start();
        UpdateHealthUI();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        HealthText.text = $"Health: {CurrentHealth}";
    }

    protected override void Die()
    {
        DeathMenuUI.SetActive(true);
        FirstPersonController.enabled = false;
        DisableShootingScripts();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableShootingScripts()
    {
        foreach (MonoBehaviour script in ShootingManager.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }
}