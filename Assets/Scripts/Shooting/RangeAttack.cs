using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class RangeAttack : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private GameObject _weaponsSlot;
    [SerializeField] private RecoilShooting _recoilShooting;
    private VisualEffect _muzzleFlash;
    private Camera _mainCamera;
    private GameObject _bulletPrefab;
    private GameObject _gunEnd;
    private RangeWeapon[] _weapons;
    private RangeWeapon _currentWeapon;
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    private float _bulletForce = 10f;
    private float _dmg = 5f;
    private float _shootRate = 1f;
    private bool _shootDelayPassed = true;
    private int _magazineCapacity = 0;
    private int _currentAmmo = 0;
    private int _backpackAmmo = 0;
    private float _reloadTime = 2;
    private bool _isReloading = false;
    private bool _isFullauto = false;

    public bool IsFullauto { get => _isFullauto; set => _isFullauto = value; }

    private void Start()
    {
        _mainCamera = Camera.main;
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Reload"].started += OnReloadClick;
        GetWeaponsData();
    }

    public void Attack()
    {
        OnFirePressed();
    }

    private void Update()
    {
        HandleAim();
    }

    private void GetWeaponsData()
    {
        _weapons = _weaponsSlot.GetComponentsInChildren<RangeWeapon>(true); // true argument to include inactive objects
        foreach (RangeWeapon weapon in _weapons)
        {
            weapon.OnWeaponChange += GetCurrentWeaponData; // Subscribe to weapon changed event
            if (weapon.gameObject.activeSelf)
            {
                GetCurrentWeaponData(weapon);
            }
        }
    }

    // Function activates on weapon enable (weapon changed)
    private void GetCurrentWeaponData(Weapon weapon)
    {
        if (weapon is RangeWeapon rangeWeapon)
        {
            _currentWeapon = rangeWeapon;
            _bulletPrefab = rangeWeapon.BulletPrefab;
            _gunEnd = rangeWeapon.GunEnd;
            _bulletForce = rangeWeapon.BulletForce;
            _dmg = rangeWeapon.Dmg;
            _shootRate = rangeWeapon.ShootRate;
            _magazineCapacity = rangeWeapon.MagazineCapacity;
            _backpackAmmo = rangeWeapon.BackpackAmmo;
            _currentAmmo = rangeWeapon.CurrentAmmo;
            _reloadTime = rangeWeapon.ReloadTime;
            _muzzleFlash = rangeWeapon.MuzzleFlash;
            _isFullauto = rangeWeapon.IsFullauto;
        }
    }

    private void OnFirePressed()
    {
        if (_currentWeapon.IsCurrentlyUsed && _shootDelayPassed && _currentAmmo > 0 && !_isReloading)
        {
            _currentAmmo--;
            UpdateAmmo(_currentAmmo, _backpackAmmo);
            StartCoroutine(ShootWithCheckFireRate());
        }
    }

    // Function must be changed when eq was implemented
    private void UpdateAmmo(int currentAmmo, int backpackAmmo)
    {
        _currentWeapon.CurrentAmmo = currentAmmo;
        _currentWeapon.BackpackAmmo = backpackAmmo;
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

        // Check if weapon is shotgun
        Shotgun shotgun = _currentWeapon.GetComponent<Shotgun>();
        if (shotgun)
        {
            int pelletCount = shotgun.Pellets;
            float splashX = shotgun.SplashX;
            float splashY = shotgun.SplashY;

            for (int i = 0; i < pelletCount; i++)
            {
                Vector3 pelletTargetDirection = targetDirection;
                pelletTargetDirection.x = Mathf.Clamp(pelletTargetDirection.x + Random.Range(-splashX, splashX), -2f, 2f);
                pelletTargetDirection.y = Mathf.Clamp(pelletTargetDirection.y + Random.Range(-splashY, splashY), -2f, 2f);
                CreateBulletWithForce(pelletTargetDirection);
            }
        }
        else
        {
            CreateBulletWithForce(targetDirection);
        }

        ShootEfects();
        ShootAnimation();
    }

    private void CreateBulletWithForce(Vector3 targetDirection)
    {
        // Creating bullet
        GameObject bullet = Instantiate(_bulletPrefab, _gunEnd.transform.position, _gunEnd.transform.rotation);
        bullet.GetComponent<Bullet>().BulletDamage = _dmg;

        // Adding force to the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(targetDirection * _bulletForce, ForceMode.Impulse);
    }

    private void ShootEfects()
    {
        // Sound
        AudioSource.PlayClipAtPoint(_currentWeapon.SoundOfShoot, _gunEnd.transform.position);

        // Visual Effect
        _recoilShooting.RecoilFire();
        _muzzleFlash.Play(); // Fire effect from the gun
        _currentWeapon.FireLight.SetActive(true); // Dynamic light fron fire effect
    }

    private void ShootAnimation()
    {
            _currentWeapon.Animator.SetTrigger("TrShoot");
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
        AudioSource.PlayClipAtPoint( _currentWeapon.SoundOfReload, transform.position);
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

    private void HandleAim()
    {
        if (_input.aim)
        {
            _currentWeapon.Animator.SetBool("isAiming", true);
        }
        else
        {
            _currentWeapon.Animator.SetBool("isAiming", false);
        }
    }
}