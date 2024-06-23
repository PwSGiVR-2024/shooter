using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class VideoEndHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string menuSceneName;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.frame = 0;
            videoPlayer.Prepare();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(menuSceneName);
    }
}