using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneHandler1 : MonoBehaviour
{
    public Transform dziad;
    public Animator anim;
    public PlayableDirector dir;
    public FirstPersonController fpc;
    private bool hasCutscenePlayed = false;
    public Light spotlight;
    public Camera WeaponCamera;
    public float WeaponTurnOnDelay;
    public GameObject shootingManager;
    private List<MonoBehaviour> shootingScripts = new List<MonoBehaviour>();

    void Start()
    {
        dir.played += OnCutsceneStarted;
        dir.stopped += OnCutsceneEnded;

        foreach (MonoBehaviour script in shootingManager.GetComponents<MonoBehaviour>())
        {
            shootingScripts.Add(script);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCutscenePlayed && other.CompareTag("Player"))
        {
            
            dir.Play();
            hasCutscenePlayed = true;
        }
    }

    private void OnCutsceneStarted(PlayableDirector dir)
    {
        spotlight.enabled = true;
        anim.SetBool("Talking", true);
        dziad.position = new Vector3(334.365f, 102.0025f, 173.702f);
        fpc.enabled = false;
        WeaponCamera.enabled = false;

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = false;
        }
    }

    private void OnCutsceneEnded(PlayableDirector pd)
    {
        spotlight.enabled = false;
        anim.SetBool("Talking", false);
        dziad.position = new Vector3(334.5662f, 101.975f, 173.6883f);
        fpc.enabled = true;
        StartCoroutine(EnableWeaponWithDelay());

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = true;
        }
    }

    private IEnumerator EnableWeaponWithDelay()
    {
        yield return new WaitForSeconds(WeaponTurnOnDelay);

        WeaponCamera.enabled = true;
    }
}