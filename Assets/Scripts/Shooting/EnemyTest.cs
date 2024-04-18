using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private float _enemyHp = 100f;

    public void TakeDamage(float damage)
    {
        _enemyHp -= damage;
        if (_enemyHp <= 0)
        {
            Die();
        }
        print(_enemyHp);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
