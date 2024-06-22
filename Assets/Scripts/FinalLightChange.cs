using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;

public class FinalLightChange : MonoBehaviour
{
    public Volume postProcessingVolume;
    public int newPriority = 1;
    public PlayableDirector dir;
    public GameObject dad;
    public Transform targetPoint;

    void Start()
    {
        if (postProcessingVolume == null)
        {
            postProcessingVolume = GetComponent<Volume>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Girl"))
        {
            dir.Play();
            postProcessingVolume.priority = newPriority;
            Debug.LogWarning("Post-Processing Activated");

            DadControllerEnd dadControllerEnd = dad.GetComponent<DadControllerEnd>();
            if (dadControllerEnd != null)
            {
                dadControllerEnd.MoveToPoint(targetPoint.position);
            }
        }
    }
}