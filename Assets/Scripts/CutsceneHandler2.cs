using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneHandler2 : MonoBehaviour
{
    public PlayableDirector dir;
    public FirstPersonController fpc;
    public Boss bossScript;
    public AudioSource soundchange;
    public GameObject enemyHealthBar;
    private bool hasCutscenePlayed = false;
    public AudioSource sound;
    public Camera WeaponCamera;
    public float WeaponTurnOnDelay;
    public Canvas crosshairCanvas;
    public GameObject shootingManager;
    private List<MonoBehaviour> shootingScripts = new List<MonoBehaviour>();
    public AudioSource wylaudio;

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
            sound.Play();
            dir.Play();
            hasCutscenePlayed = true;
        }
    }

    private void OnCutsceneStarted(PlayableDirector dir)
    {
        fpc.enabled = false;
        WeaponCamera.enabled = false;
        crosshairCanvas.enabled = false;

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = false;
        }
    }

    private void OnCutsceneEnded(PlayableDirector pd)
    {
        fpc.enabled = true;
        bossScript.enabled = true;
        enemyHealthBar.SetActive(true);
        StartCoroutine(EnableWeaponWithDelay());
        soundchange.Play();
        wylaudio.Stop();

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = true;
        }
    }

    private IEnumerator EnableWeaponWithDelay()
    {
        yield return new WaitForSeconds(WeaponTurnOnDelay);

        WeaponCamera.enabled = true;
        crosshairCanvas.enabled = true;
    }
}