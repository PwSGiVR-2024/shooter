using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private BossFight _boss;

    void Start()
    {
        Debug.Log("BossHealthBar Start method called");
        _boss = FindObjectOfType<BossFight>();
        if (_boss == null)
        {
            Debug.LogError("BossFight component not found!");
            return;
        }
        healthSlider.maxValue = _boss.MaxHealth;
        healthSlider.value = _boss.CurrentHealth;
        _boss.OnBossHealthChanged += UpdateHealthBar;
        Debug.Log($"Subscribed to OnBossHealthChanged. Initial health: {_boss.CurrentHealth}");
    }

    void UpdateHealthBar(int currentHealth)
    {
        Debug.Log($"Updating health bar: {currentHealth}");
        healthSlider.value = currentHealth;
    }
}