using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private Boss boss;

    void Start()
    {
        boss = FindObjectOfType<Boss>();

        healthSlider.maxValue = boss.MaxHealth;
        healthSlider.value = boss.CurrentHealth;

        boss.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}