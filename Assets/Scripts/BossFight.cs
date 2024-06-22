using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;
    public GameObject minionPrefab;
    public Transform[] spawnPoints;
    private bool canTakeDamage = true;
    private List<GameObject> spawnedMinions = new List<GameObject>();
    private Animator anim;
    private bool isDead = false;
    public event Action<int> OnHealthChanged;
    public event Action OnBossDeath;
    public GameObject fence;
    public GirlControllerEnd girl;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        OnHealthChanged?.Invoke(currentHealth);
    }

    void Update()
    {
        if (isDead)
            return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(50);
        }

        CheckMinions();
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;

        int damageInt = (int)damage;
        currentHealth -= damageInt;

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        if (currentHealth % 250 == 0)
        {
            SpawnMinions();
        }
    }

    void SpawnMinions()
    {
        canTakeDamage = false;

        for (int i = 0; i < 4; i++)
        {
            GameObject minion = Instantiate(minionPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            spawnedMinions.Add(minion);
        }
    }

    void CheckMinions()
    {
        if (!canTakeDamage && spawnedMinions.Count > 0)
        {
            spawnedMinions.RemoveAll(minion => minion == null);
            if (spawnedMinions.Count == 0)
            {
                canTakeDamage = true;
            }
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        OnBossDeath?.Invoke();

        fence.GetComponent<FenceController>().Sink();

        girl.MoveToTarget();

        Invoke(nameof(HideHealthBar), 2f);
    }

    void HideHealthBar()
    {
        GameObject healthBar = GameObject.Find("EnemyHealthBar");
        healthBar.SetActive(false);
    }
}