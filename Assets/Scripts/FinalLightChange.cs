using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;

public class FinalLightChange : MonoBehaviour
{
    public Volume PostProcessingVolume;
    public int NewPriority = 1;
    public PlayableDirector Dir;
    public GameObject Dad;
    public GameObject Girl;
    public Transform TargetPoint;
    public FirstPersonController Fpc;
    public Camera WeaponCamera;
    public float WeaponTurnOnDelay;
    public Canvas CrosshairCanvas;
    public GameObject ShootingManager;
    private readonly List<MonoBehaviour> shootingScripts = new List<MonoBehaviour>();

    void Start()
    {
        Dir.played += OnCutsceneStarted;
        Dir.stopped += OnCutsceneEnded;

        if (PostProcessingVolume == null)
        {
            PostProcessingVolume = GetComponent<Volume>();
        }

        foreach (MonoBehaviour script in ShootingManager.GetComponents<MonoBehaviour>())
        {
            shootingScripts.Add(script);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Girl"))
        {
            Dir.Play();
            PostProcessingVolume.priority = NewPriority;
            Debug.LogWarning("Post-Processing Activated");

            DadControllerEnd dadControllerEnd = Dad.GetComponent<DadControllerEnd>();
            if (dadControllerEnd != null)
            {
                dadControllerEnd.MoveToPoint(TargetPoint.position);
            }
        }
    }

    private void OnCutsceneStarted(PlayableDirector dir)
    {
        Fpc.enabled = false;
        WeaponCamera.enabled = false;
        CrosshairCanvas.enabled = false;

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = false;
        }
    }

    private void OnCutsceneEnded(PlayableDirector pd)
    {
        Fpc.enabled = true;
        StartCoroutine(EnableWeaponWithDelay());

        foreach (MonoBehaviour script in shootingScripts)
        {
            script.enabled = true;
        }

        GirlControllerEnd girlControllerEnd = Girl.GetComponent<GirlControllerEnd>();
        if (girlControllerEnd != null)
        {
            girlControllerEnd.EndTalking();
        }
    }

    private IEnumerator EnableWeaponWithDelay()
    {
        yield return new WaitForSeconds(WeaponTurnOnDelay);
        WeaponCamera.enabled = true;
        CrosshairCanvas.enabled = true;

        SceneController.instance.NextLevel();
    }
}