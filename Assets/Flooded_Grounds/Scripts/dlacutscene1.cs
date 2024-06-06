using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class dlacutscene1 : MonoBehaviour
{
    public Transform dziad;
    public Animator anim;
    public PlayableDirector dir;

    void Start()
    {
        dir.played += OnCutsceneStarted;
        dir.stopped += OnCutsceneEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        dir.Play();
    }

    private void OnCutsceneStarted(PlayableDirector dir)
    {
        anim.SetBool("Talking", true);
        dziad.position = new Vector3(334.365f, 102.0025f, 173.702f);
    }

    private void OnCutsceneEnded(PlayableDirector pd)
    {
        anim.SetBool("Talking", false);
        dziad.position = new Vector3(334.5662f, 101.975f, 173.6883f);
    }
}