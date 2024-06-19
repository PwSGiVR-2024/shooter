using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditively : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("PG", LoadSceneMode.Additive);
    }
}
