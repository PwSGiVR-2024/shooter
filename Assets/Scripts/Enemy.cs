using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 2.5f;
    [SerializeField] private float _detectionRange = 30.0f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackRange = 1.25f;
    [SerializeField] private float _attackCooldown = 1.5f;
    private Transform _playerTransform;
    private PlayerHealth _playerHealth;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Vector3 _lastKnownPlayerPosition;
    private bool _isDead = false;
    private bool _canAttack = true;

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerHealth = _playerTransform.GetComponentInChildren<PlayerHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemySpeed;
        _anim = GetComponent<Animator>();
        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        enemyHealth.OnEnemyDeath += Die;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        DebugInput();
        TryAttackPlayer();
    }

    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Die();
            return;
        }
    }

    protected void Die()
    {
        _isDead = true;
        _agent.enabled = false;
        _anim.SetTrigger("Die");
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        int ghoulRemainsCount = Random.Range(0, 3);
        for (int i = 0; i < ghoulRemainsCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().GhoulRemains, transform.position + offset, transform.rotation);
        }
        int rottenApplesCount = Random.Range(0, 4);
        for (int i = 0; i < rottenApplesCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(GameObject.Find("ItemPrefabs").GetComponent<ItemPrefabs>().RottenApple, transform.position + offset, transform.rotation);
        }
        Destroy(gameObject, 2f);
    }

    private void TryAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= _detectionRange)
        {
            _lastKnownPlayerPosition = _playerTransform.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            _agent.destination = _lastKnownPlayerPosition;
        }
        if (_agent.velocity.magnitude > 0.1f)
        {
            _anim.SetBool("Walking", true);
        }
        else
        {
            _anim.SetBool("Walking", false);
        }
        if (distanceToPlayer <= _attackRange && _canAttack)
        {
            _anim.SetTrigger("Attack");
            StartCoroutine(AttackWithCooldown());
        }
    }

    private IEnumerator AttackWithCooldown()
    {
        _canAttack = false;
        AttackPlayer();
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private void AttackPlayer()
    {
        _playerHealth.TakeDamage(_attackDamage);
    }
}