using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAdditively : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("PG", LoadSceneMode.Additive);
    }
}
