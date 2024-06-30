using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInCave : MonoBehaviour
{
    public AudioSource girlsound;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed)
        {
            girlsound.Play();
            hasPlayed = true;
        }
    }
}