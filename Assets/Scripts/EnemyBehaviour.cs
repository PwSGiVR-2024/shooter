using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player; // Przypisz transform gracza w edytorze Unity
    public float speed = 5.0f; // Prêdkoœæ, z jak¹ przeciwnik porusza siê w kierunku gracza
    public float sightRange = 100.0f; // Maksymalny zasiêg widzenia przeciwnika
    public Vector3 eyeLevelOffset = new Vector3(0, 0.7f, 0); // Przesuniêcie od centrum przeciwnika do "poziomu oczu"
    private Vector3 lastKnownPlayerPosition;
    private bool playerInSight = false;

    void Update()
    {
        RaycastHit hit;
        Vector3 startRaycastPosition = transform.position + eyeLevelOffset;
        Vector3 direction = player.position - startRaycastPosition;

        Debug.DrawLine(startRaycastPosition, startRaycastPosition + direction.normalized * sightRange, Color.red);

        if (Physics.Raycast(startRaycastPosition, direction.normalized, out hit, sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lastKnownPlayerPosition = player.position;
                playerInSight = true;
                MoveTowardsPlayer(player.position); // Move towards the current position of the player
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }

        // If the player is out of sight, move to the last known position
        if (!playerInSight && lastKnownPlayerPosition != Vector3.zero)
        {
            MoveTowardsPlayer(lastKnownPlayerPosition);
        }
    }

    void MoveTowardsPlayer(Vector3 targetPosition)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        // Optionally, clear the last known position once reached, to stop moving
        if (transform.position == lastKnownPlayerPosition)
        {
            lastKnownPlayerPosition = Vector3.zero; // Reset the last known position to stop the enemy
        }
    }
}