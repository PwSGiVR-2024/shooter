using UnityEngine;


// This class exists only for make develop easier
// Delete this class after implementing the eq system
public class WeaponSwitcher : MonoBehaviour
{
    public Weapon Key1;
    public Weapon Key2;
    public Weapon Key3;
    public Weapon Key4;

    public Weapon[] _keys;

    public static WeaponSwitcher Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _keys = new Weapon[] { Key1, Key2, Key3, Key4 };
    }

    public void SetWeaponActive(Weapon weapon)
    {
        foreach (var key in _keys)
        {
            if (key == weapon && weapon.IsUnlocked)
            {
                key.gameObject.SetActive(true);
                if (weapon is RangeWeapon)
                {
                    WeaponManager.Instance.SetAttackStrategy(GetComponent<RangeWeaponController>());
                }
                else if (weapon is MeleeWeapon)
                {
                    WeaponManager.Instance.SetAttackStrategy(GetComponent<MeleeWeaponController>());
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
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeaponActive(Key4);
        }
    }
}
