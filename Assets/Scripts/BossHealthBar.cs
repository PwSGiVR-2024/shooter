using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private Boss _boss;

    void Start()
    {
        _boss = FindObjectOfType<Boss>();

        healthSlider.maxValue = _boss.MaxHealth;
        healthSlider.value = _boss.CurrentHealth;

        _boss.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}