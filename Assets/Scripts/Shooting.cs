using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Shooting : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject _bulletPrefab;
    private GameObject _gunEnd;
    private InputSystemUIInputModule _inputModule;
    private Weapon[] _weapons;
    private Weapon _currentWeapon;
    private float _bulletForce = 10f;
    private float _dmg = 5f;



    private void Start()
    {
        _inputModule = GameObject.FindObjectOfType<InputSystemUIInputModule>();
        _mainCamera = Camera.main;
        GetWeaponsData();
        
        // Subscribe to the left click event
        _inputModule.leftClick.action.performed += OnLeftClick;
    }

    private void GetWeaponsData()
    {
        _weapons = GetComponentsInChildren<Weapon>(true); // true argument to include inactive objects
        foreach (Weapon weapon in _weapons)
        {
            weapon.OnWeaponEnable += GetCurrentWeaponData; // Subscribe to weapon changed event
            if (weapon.gameObject.activeSelf)
            {
                GetCurrentWeaponData(weapon);
            }
        }
    }

    // Function activates on weapon enable (weapon changed)
    private void GetCurrentWeaponData(Weapon weapon)
    {
        _currentWeapon = weapon;
        _bulletPrefab = weapon.BulletPrefab;
        _gunEnd = weapon.GunEnd;
        _bulletForce = weapon.BulletForce;
        _dmg = weapon.Dmg;
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        // ----------------  TO DO : Add conditions to check if the player can shoot ---------------------
        if (_currentWeapon.IsCurrentlyUsed)
        {
            Shoot();   
        }
    }

    private void Shoot()
    {
        // Getting shoot direction
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 targetDirection = ray.direction;

        GameObject bullet = Instantiate(_bulletPrefab, _gunEnd.transform.position, _gunEnd.transform.rotation);
        bullet.GetComponent<Bullet>().SetBulletDamage(_dmg);

        // Adding force to the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(targetDirection * _bulletForce, ForceMode.Impulse);
    }
}
