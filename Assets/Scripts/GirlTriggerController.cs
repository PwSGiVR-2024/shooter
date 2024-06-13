using UnityEngine;

public class GirlTriggerController : MonoBehaviour
{
    public GirlController girlController1;
    public GirlController girlController2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            girlController1.StartMoving();
            girlController2.StartMoving();
        }
    }
}