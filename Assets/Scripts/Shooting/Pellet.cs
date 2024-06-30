using UnityEngine;

public class Pellet : Bullet
{
    [SerializeField] private float _dmgTimeDecreaser = 1;
    private float _activationTime;
    private float _aliveTime;
    private float _dmgDecreased;

    private void OnEnable()
    {
        _activationTime = Time.time;
    }

    private void Start()
    {
        _dmgDecreased = BulletDamage;
    }

    private void LateUpdate()
    {
        _aliveTime = Time.time - _activationTime;
        _dmgDecreased = Mathf.Clamp(_dmgDecreased - (_aliveTime * _dmgTimeDecreaser), 0, BulletDamage);
        BulletDamage = (int)_dmgDecreased;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Pellet pellet = collision.gameObject.GetComponent<Pellet>();
        if (pellet)
        {
            return;
        }
        // Takes the rest of function from Bullet class
        base.OnCollisionEnter(collision);
    }
}