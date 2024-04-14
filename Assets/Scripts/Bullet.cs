using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroyBullet = 10f;


    private void Start()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }

    private IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(_timeToDestroyBullet);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyTest enemy = collision.gameObject.GetComponent<EnemyTest>();
        if (enemy)
        {
            enemy.TakeDamage(1);
        }

        DestroyBullet();
    }
}
