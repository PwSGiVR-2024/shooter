using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroyBullet = 10f;
    private float _bulletDamage = 1f;

    public float BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }


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

    protected virtual void OnCollisionEnter(Collision collision)
    {
        EnemyNavigation enemy = collision.gameObject.GetComponent<EnemyNavigation>();
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (enemy)
        {
            enemy.EnemyTakeDamage(_bulletDamage);
        } else if (boss)
        {
            boss.TakeDamage(_bulletDamage);
        }

        DestroyBullet();
    }
}
