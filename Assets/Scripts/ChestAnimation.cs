using UnityEngine;
using UnityEngine.Playables;

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
            anim.SetTrigger("ChestOpen");
        }
    }
}
