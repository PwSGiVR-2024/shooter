using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 2.5f;
    [SerializeField] private float _detectionRange = 30.0f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackRange = 1.25f;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _ghoulIdle;
    [SerializeField] private AudioClip _ghoulWalk;
    [SerializeField] private float _soundInterval = 5f;
    [SerializeField] private float _idleSoundRange = 15f;

    private Transform _playerTransform;
    private PlayerHealth _playerHealth;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Vector3 _lastKnownPlayerPosition;
    private bool _isDead = false;
    private bool _canAttack = true;
    private bool _wasSoundPlayed = false;
    private bool _hasStartedChasing = false;


    private void Start()
    {
        _anim = GetComponent<Animator>();

        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerHealth = _playerTransform.GetComponentInChildren<PlayerHealth>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemySpeed;

        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        enemyHealth.OnEnemyDeath += Die;
        enemyHealth.OnEnemyTakeDamage += TakeDamage;

    }

    private void TakeDamage()
    {
        if (WeaponManager.Instance.CurrentWeapon is MeleeWeapon)
        {
            StartCoroutine(PlayHitWithDelay(0.4f));
        }
        else
        {
            StartCoroutine(PlayHitWithDelay(0f));
        }
    }

    private IEnumerator PlayHitWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(_hitSound, gameObject.transform.position);
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        DebugInput();
        TryAttackPlayer();
        TryPlayIdleSound();
    }

    private void TryPlayIdleSound()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _idleSoundRange && _agent.velocity.magnitude < 0.1f)
        {
            if (!_wasSoundPlayed)
            {
                PlayGhoulSound(_ghoulIdle);
                StartCoroutine(SoundCooldown());
            }
        }
    }

    private IEnumerator SoundCooldown()
    {
        _wasSoundPlayed = true;
        yield return new WaitForSeconds(_soundInterval);
        _wasSoundPlayed = false;
    }

    private void PlayGhoulSound(AudioClip clip)
    {
        AudioManager.Instance.PlaySound(clip, gameObject.transform.position);
    }

    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        _isDead = true;
        GameManager.Instance.AudioManager.PlaySound(_deathSound, gameObject.transform.position);
        _agent.enabled = false;
        _anim.SetTrigger("Die");
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        GameObject spawner = new(name + "DropSpawner");
        spawner.transform.position = transform.position;
        spawner.AddComponent<GhoulDropSpawner>();
        spawner.GetComponent<GhoulDropSpawner>().Spawn(2.5f);

        Destroy(gameObject, 2f);
    }

    private void TryAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= _detectionRange)
        {
            if (!_hasStartedChasing)
            {
                PlayGhoulSound(_ghoulWalk);
                _hasStartedChasing = true;
            }
            _lastKnownPlayerPosition = _playerTransform.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            _agent.destination = transform.position;
            _hasStartedChasing = false;
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
            StartCoroutine(AttackWithCooldown());
        }
    }

    private IEnumerator AttackWithCooldown()
    {
        _canAttack = false;
        StartCoroutine(AttackPlayerWithDelay());
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private IEnumerator AttackPlayerWithDelay()
    {
        _anim.SetTrigger("Attack");
        AudioManager.Instance.PlaySound(_attackSound, gameObject.transform.position);
        yield return new WaitForSeconds(0.8f);
        _playerHealth.TakeDamage(_attackDamage);
    }

    //private void PlaySound(AudioClip clip)
    //{
    //    if (_audioSource != null && clip != null)
    //    {
    //        _audioSource.PlayOneShot(clip);
    //    }
    //}
}