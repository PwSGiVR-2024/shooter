using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject _bulletPrefab;
    private GameObject _gunEnd;
    private Weapon[] _weapons;
    private Weapon _currentWeapon;
    private float _bulletForce = 10f;
    private float _dmg = 5f;
    private float _shootRate = 1f;
    private bool _shootDelayPassed = true;


    private void Start()
    {
        _mainCamera = Camera.main;
        PlayerInput playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerInput.actions["Fire"].started += OnLeftClick; // Subscribe the left click event to Input System 
        GetWeaponsData();
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
        _shootRate = weapon.ShootRate;
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        if (_currentWeapon.IsCurrentlyUsed && _shootDelayPassed)
        {
            StartCoroutine(ShootWithCheckFireRate());
        }
    }

    private IEnumerator ShootWithCheckFireRate()
    {
        _shootDelayPassed = false;
        Shoot();
        yield return new WaitForSeconds(_shootRate);
        _shootDelayPassed = true;
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
