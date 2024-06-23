using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;

public class FinalLightChange : MonoBehaviour
{
    public Volume postProcessingVolume;
    public int newPriority = 1;
    public PlayableDirector dir;
    public GameObject dad;
    public Transform targetPoint;
    public FirstPersonController fpc;
    public Camera WeaponCamera;
    public float WeaponTurnOnDelay;
    public Canvas crosshairCanvas;
    public GameObject shootingManager;
    private List<MonoBehaviour> shootingScripts = new List<MonoBehaviour>();

    void Start()
    {
        dir.played += OnCutsceneStarted;
        dir.stopped += OnCutsceneEnded;

        if (postProcessingVolume == null)
        {
            postProcessingVolume = GetComponent<Volume>();
        }

        foreach (MonoBehaviour script in shootingManager.GetComponents<MonoBehaviour>())
        {
            shootingScripts.Add(script);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Girl"))
        {
            dir.Play();
            postProcessingVolume.priority = newPriority;
            Debug.LogWarning("Post-Processing Activated");

            DadControllerEnd dadControllerEnd = dad.GetComponent<DadControllerEnd>();
            if (dadControllerEnd != null)
            {
                dadControllerEnd.MoveToPoint(targetPoint.position);
            }
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
        crosshairCanvas.enabled = true;

        SceneController.instance.NextLevel();
    }
}