using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditively : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("PG", LoadSceneMode.Additive);
    }
}
