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
    private int _magazineCapacity = 0;
    private int _currentAmmo = 0;
    private int _backpackAmmo = 0;
    private float _reloadTime = 2;
    private bool _isReloading = false;


    private void Start()
    {
        _mainCamera = Camera.main;
        PlayerInput playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerInput.actions["Fire"].started += OnLeftClick; // Subscribe the left click event to Input System
        playerInput.actions["Reload"].started += OnReloadClick;
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
        _magazineCapacity = weapon.MagazineCapacity;
        _backpackAmmo = weapon.BackpackAmmo;
        _currentAmmo = weapon.CurrentAmmo;
        _reloadTime = weapon.ReloadTime;
    }

    // Function must be changed when eq was implemented
    private void UpdateAmmo(int currentAmmo, int backpackAmmo)
    {
        _currentWeapon.CurrentAmmo = currentAmmo;
        _currentWeapon.BackpackAmmo = backpackAmmo;
        print(_currentWeapon.CurrentAmmo);
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        if (_currentWeapon.IsCurrentlyUsed && _shootDelayPassed && _currentAmmo > 0 && !_isReloading)
        {
            _currentAmmo--;
            UpdateAmmo(_currentAmmo, _backpackAmmo);
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


    private void OnReloadClick(InputAction.CallbackContext context)
    {
        if (_backpackAmmo > 0 && _magazineCapacity != _currentAmmo)
        {
            StartCoroutine(StartReloading());
        }
    }

    private IEnumerator StartReloading()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        Reload();
        _isReloading = false;
    }

    private void Reload()
    {
        int neededAmmo = _magazineCapacity - _currentAmmo; // How many bullets we need
        if (neededAmmo < _backpackAmmo) // If we need less then we have
        {
            _currentAmmo = _magazineCapacity;
            _backpackAmmo -= neededAmmo;
        }
        else if (neededAmmo > _backpackAmmo) // If we need more then we have
        {
            _currentAmmo += _backpackAmmo;
            _backpackAmmo = 0;
        }
        UpdateAmmo(_currentAmmo, _backpackAmmo);
    }
}