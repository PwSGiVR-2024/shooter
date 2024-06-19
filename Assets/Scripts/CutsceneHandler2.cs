using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneHandler2 : MonoBehaviour
{
    public PlayableDirector dir;
    public FirstPersonController fpc;
    public Boss bossScript;
    public GameObject enemyHealthBar;
    private bool hasCutscenePlayed = false;

    void Start()
    {
        dir.played += OnCutsceneStarted;
        dir.stopped += OnCutsceneEnded;
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
        fpc.enabled = false;
    }

    private void OnCutsceneEnded(PlayableDirector pd)
    {
        fpc.enabled = true;
        bossScript.enabled = true;
        enemyHealthBar.SetActive(true);
    }
}