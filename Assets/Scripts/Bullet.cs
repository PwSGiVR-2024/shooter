using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EnemyTest enemy = collision.gameObject.GetComponent<EnemyTest>();
        if (enemy)
        {
            enemy.TakeDamage(1);
        }
        
    }
}
