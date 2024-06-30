using System.Collections;
using UnityEngine;

public class DisableLight : MonoBehaviour
{
    [SerializeField] private float _disableTime = 0.1f;
    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_disableTime);
        gameObject.SetActive(false);
    }
}
