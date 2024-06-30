using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Opened", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Opened", false);
        }
    }
}