using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform Player; // Assign a player transform in the Unity editor
    public float Speed = 2.5f; // The speed at which the enemy moves towards the player
    public float SightRange = 100.0f; // Maximum enemy view range
    public Vector3 EyeLevelOffset = new Vector3(0, 0.7f, 0); // Moving from the opponent's center to "eye level"
    private Vector3 _lastKnownPlayerPosition;
    private bool _playerInSight = false;

    void Update()
    {
        Vector3 startRaycastPosition = transform.position + EyeLevelOffset;
        Vector3 direction = Player.position - startRaycastPosition;

        Debug.DrawLine(startRaycastPosition, startRaycastPosition + direction.normalized * SightRange, Color.red);

        if (Physics.Raycast(startRaycastPosition, direction.normalized, out RaycastHit hit, SightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                _lastKnownPlayerPosition = Player.position;
                _playerInSight = true;
                MoveTowardsPlayer(Player.position); // Move towards the current position of the player
            }
            else
            {
                _playerInSight = false;
            }
        }
        else
        {
            _playerInSight = false;
        }

        // If the player is out of sight, move to the last known position
        if (!_playerInSight && _lastKnownPlayerPosition != Vector3.zero)
        {
            MoveTowardsPlayer(_lastKnownPlayerPosition);
        }
    }

    void MoveTowardsPlayer(Vector3 targetPosition)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }

        // Optionally, clear the last known position once reached, to stop moving
        if (transform.position == _lastKnownPlayerPosition)
        {
            _lastKnownPlayerPosition = Vector3.zero; // Reset the last known position to stop the enemy
        }
    }
}