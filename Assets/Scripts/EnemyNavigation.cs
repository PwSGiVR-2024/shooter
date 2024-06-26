using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    Animator anim;
    private Transform Player;
    private NavMeshAgent _agent;
    public float DetectionRange = 30.0f;
    private Vector3 _lastKnownPlayerPosition;
    public float EnemySpeed = 2.5f;
    public float AttackRange = 1.25f;
    public float Health = 100.0f;
    private bool isDead = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = EnemySpeed;
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead)
            return;

        if (Input.GetKeyDown(KeyCode.H))
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer <= DetectionRange)
        {
            _lastKnownPlayerPosition = Player.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            _agent.destination = _lastKnownPlayerPosition;
        }

        if (_agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (distanceToPlayer <= AttackRange)
        {
            anim.SetTrigger("Attack");
        }

    }

    public void EnemyTakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        _agent.enabled = false;
        anim.SetTrigger("Die");

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
}