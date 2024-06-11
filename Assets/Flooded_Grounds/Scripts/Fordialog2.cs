using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Fordialog2 : MonoBehaviour
{
    public PlayableDirector dir;

    private void OnTriggerEnter(Collider other)
    {
        dir.Play();
    }

}
