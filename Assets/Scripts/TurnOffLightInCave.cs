using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLightInCave : MonoBehaviour
{
    public Light directionallight;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            directionallight.color = Color.black;
        }
    }
}