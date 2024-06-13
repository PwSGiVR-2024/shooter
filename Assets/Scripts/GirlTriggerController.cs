using UnityEngine;

public class GirlTriggerController : MonoBehaviour
{
    public GirlController girlController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            girlController.StartMoving();
        }
    }
}