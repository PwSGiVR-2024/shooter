using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Playables;

public class FinalLightChange : MonoBehaviour
{
    public Volume postProcessingVolume;
    public int newPriority = 1;
    public PlayableDirector dir;
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
            Debug.LogWarning("Post-Processing ");
        }
    }
}
