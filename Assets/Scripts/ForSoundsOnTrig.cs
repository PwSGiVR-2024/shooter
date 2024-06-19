using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSoundsOnTrig : MonoBehaviour
{
    public AudioSource sound;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed)
        {
            sound.Play();
            hasPlayed = true;
        }
    }
}