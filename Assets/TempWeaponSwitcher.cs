using UnityEngine;


// This class exists only for make develop easier
// Delete this class after implementing the eq system
public class TempWeaponSwitcher : MonoBehaviour
{
    public GameObject FullAuto;
    public GameObject TapFire;
    public GameObject Shotgun;

    private void Start()
    {
        FullAuto.SetActive(false);
        TapFire.SetActive(false);
        Shotgun.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FullAuto.SetActive(true);
            TapFire.SetActive(false);
            Shotgun.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FullAuto.SetActive(false);
            TapFire.SetActive(true);
            Shotgun.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FullAuto.SetActive(false);
            TapFire.SetActive(false);
            Shotgun.SetActive(true);
        }
    }   
}
