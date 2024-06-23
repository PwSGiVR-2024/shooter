using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskForShop : MonoBehaviour
{
    public AudioSource sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sound.Play();
            
        }
    }
}