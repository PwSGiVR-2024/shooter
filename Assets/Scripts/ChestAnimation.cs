using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class dlacchest : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
        GetComponent<PlayableDirector>().Play();
   }
    
}

