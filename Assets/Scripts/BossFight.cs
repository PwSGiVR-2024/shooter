using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int MaxHealth = 1000;
    public int CurrentHealth;
    public GameObject MinionPrefab;
    public Transform[] SpawnPoints;
    private bool _canTakeDamage = true;
    private readonly List<GameObject> spawnedMinions = new List<GameObject>();
    private Animator _anim;
    private bool _isDead = false;
    public event Action<int> OnHealthChanged;
    public event Action OnBossDeath;
    public GameObject Fence;
    public GirlControllerEnd Girl;

    void Start()
    {
        CurrentHealth = MaxHealth;
        _anim = GetComponent<Animator>();
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    void Update()
    {
        if (_isDead)
            return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(50);
        }

        CheckMinions();
    }

    public void TakeDamage(float damage)
    {
        if (!_canTakeDamage)
            return;

        int damageInt = (int)damage;
        CurrentHealth -= damageInt;

        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        if (CurrentHealth % 250 == 0)
        {
            SpawnMinions();
        }
    }

    void SpawnMinions()
    {
        _canTakeDamage = false;

        for (int i = 0; i < 4; i++)
        {
            GameObject minion = Instantiate(MinionPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            spawnedMinions.Add(minion);
        }
    }

    void CheckMinions()
    {
        if (!_canTakeDamage && spawnedMinions.Count > 0)
        {
            spawnedMinions.RemoveAll(minion => minion == null);
            if (spawnedMinions.Count == 0)
            {
                _canTakeDamage = true;
            }
        }
    }

    void Die()
    {
        _isDead = true;
        _anim.SetTrigger("Die");

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        OnBossDeath?.Invoke();

        Fence.GetComponent<FenceController>().Sink();

        Girl.MoveToTarget();

        Invoke(nameof(HideHealthBar), 2f);
    }

    void HideHealthBar()
    {
        GameObject healthBar = GameObject.Find("EnemyHealthBar");
        healthBar.SetActive(false);
    }
}