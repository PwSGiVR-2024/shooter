using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    Animator anim; // Reference to the Animator component
    public Transform Player; // Reference to the player's transform
    private NavMeshAgent _agent; // Reference to the NavMeshAgent component
    public float DetectionRange = 30.0f; // Detection range for the enemy
    private Vector3 _lastKnownPlayerPosition; // Last known position of the player
    public float EnemySpeed = 2.5f; // Speed of the enemy
    public float AttackRange = 2.0f; // Attack range of the enemy
    public float Health = 100.0f; // Health of the enemy
    private bool isDead = false; // Check if the enemy is dead

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = EnemySpeed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // If the enemy is dead, stop updating
        if (isDead)
            return;

        // Temporarily kill the enemy
        if (Input.GetKeyDown(KeyCode.H))
        {
            Die();
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        // If the player is within detection range
        if (distanceToPlayer <= DetectionRange)
        {
            // Update the last known player position and set it as the destination
            _lastKnownPlayerPosition = Player.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            // Continue moving towards the last known player position
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

    // Method to apply damage to the enemy
    public void EnemyTakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0f)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    void Die()
    {
        isDead = true;
        _agent.enabled = false;
        anim.SetTrigger("Die");

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Destroy(gameObject, 5f);
    }
}