using UnityEngine;

public class FenceController : MonoBehaviour
{
    public float sinkSpeed = 2.0f;
    private bool isSinking = false;

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void Sink()
    {
        isSinking = true;
        Destroy(gameObject, 5f);
    }
}