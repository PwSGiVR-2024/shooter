using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioincave : MonoBehaviour
{
    public AudioSource girlsound;
    private void OnTriggerEnter(Collider other)
    {
        
        girlsound.Play();

    }

}
