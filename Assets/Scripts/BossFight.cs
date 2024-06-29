using System;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : EnemyHealth
{
    public GameObject MinionPrefab;
    public Transform[] SpawnPoints;
    public event Action<int> OnBossHealthChanged;
    public event Action OnBossDeath;
    public GameObject Fence;
    public GirlControllerEnd Girl;
    public int _lastHealthThreshold;

    private bool _canTakeDamage = true;
    private readonly List<GameObject> spawnedMinions = new List<GameObject>();
    private Animator _anim;
    private bool _isDead = false;


    private void Update()
    {
        if(_isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(50);
            OnBossHealthChanged?.Invoke(CurrentHealth);
        }

        CheckMinions();
    }
    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        OnBossHealthChanged?.Invoke(CurrentHealth);
        _lastHealthThreshold = CurrentHealth / 250;
    }

    public override void TakeDamage(int damage)
    {
        if (!_canTakeDamage)
        {
            Debug.Log("Can't take damage");
            return;
        }
        Debug.Log($"Taking damage: {damage}. Current health: {CurrentHealth}");
        base.TakeDamage(damage);
        Debug.Log($"After damage: {CurrentHealth}");
        OnBossHealthChanged?.Invoke(CurrentHealth);

        int newHealthThreshold = (CurrentHealth / 250);
        if (newHealthThreshold < _lastHealthThreshold)
        {
            _lastHealthThreshold = newHealthThreshold;
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

    protected override void Die()
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