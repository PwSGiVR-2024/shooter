using UnityEngine;

public class Pellet : Bullet
{
    [SerializeField] private float _dmgTimeDecreaser = 1;
    private float _activationTime;
    private float _aliveTime;


    private void OnEnable()
    {
        _activationTime = Time.time;
    }

    private void LateUpdate()
    {
        _aliveTime = Time.time - _activationTime;
        BulletDamage -= Mathf.Clamp(_aliveTime * _dmgTimeDecreaser, 0, BulletDamage);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Pellet pellet = collision.gameObject.GetComponent<Pellet>();
        if (pellet)
        {
            return;
        }
        //print("Single bullet will gives: " + BulletDamage);

        // Takes the rest of function from Bullet class
        base.OnCollisionEnter(collision);
    }
}
