using UnityEngine;


// This class exists only for make develop easier
// Delete this class after implementing the eq system
public class TempWeaponSwitcher : MonoBehaviour
{
    public Weapon Key1;
    public Weapon Key2;
    public Weapon Key3;

    private Weapon[] _keys;

    private void Start()
    {
        _keys = new Weapon[] { Key1, Key2, Key3 };
        SetWeaponActive(Key1);
    }

    public void SetWeaponActive(Weapon weapon)
    {
        foreach (var key in _keys)
        {
            if (key == weapon)
            {
                key.gameObject.SetActive(true);
                if (weapon is RangeWeapon)
                {
                    WeaponManager.Instance.SetAttackStrategy(GetComponent<RangeAttack>());
                }
                else if (weapon is MeleeWeapon){
                    WeaponManager.Instance.SetAttackStrategy(GetComponent<MeleeAttack>());
                }
            }
            else
            {
                key.gameObject.SetActive(false);
            }
        }
        
    }

    // Changing the weapons
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeaponActive(Key1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeaponActive(Key2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeaponActive(Key3);
        }
    }
}
