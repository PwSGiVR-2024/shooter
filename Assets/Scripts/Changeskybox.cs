using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changeskybox : MonoBehaviour
{
    public Material night;
    private void OnTriggerEnter(Collider other)
    {

        RenderSettings.skybox = night;

    }
}
