using UnityEngine;

public class Pellet : Bullet
{
    protected override void OnCollisionEnter(Collision collision)
    {
        Pellet pellet = collision.gameObject.GetComponent<Pellet>();
        if (pellet)
        {
            return;
        }

        base.OnCollisionEnter(collision);
    }
}
