using UnityEngine;


// This class exists only for make develop easier
// Delete this class after implementing the eq system
public class TempWeaponSwitcher : MonoBehaviour
{
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;

    private void Start()
    {
        Key1.SetActive(true);
        Key2.SetActive(false);
        Key3.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Key1.SetActive(true);
            Key2.SetActive(false);
            Key3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Key1.SetActive(false);
            Key2.SetActive(true);
            Key3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Key1.SetActive(false);
            Key2.SetActive(false);
            Key3.SetActive(true);
        }
    }
}
