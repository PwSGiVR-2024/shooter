using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private BossFight _boss;

    void Start()
    {
        _boss = FindObjectOfType<BossFight>();
        if (_boss == null)
        {
            return;
        }
        healthSlider.maxValue = _boss.MaxHealth;
        healthSlider.value = _boss.CurrentHealth;
        _boss.OnBossHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}