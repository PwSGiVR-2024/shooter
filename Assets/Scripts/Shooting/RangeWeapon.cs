using UnityEngine;
using System;
using UnityEngine.VFX;

public class RangeWeapon : Weapon
{
    public event Action<RangeWeapon> OnWeaponEnable;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _gunEnd;
    [SerializeField] private GameObject _fireLight;
    [SerializeField] private AudioClip _soundOfShoot;
    [SerializeField] private AudioClip _soundOfReload;
    [SerializeField] private VisualEffect _muzzleFlash;
    [SerializeField] private float _bulletForce = 10f;
    [SerializeField] private float _shootRate = 0.5f; // interval between shots in seconds
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private int _magazineCapacity = 30; // max ammo in magazine after reload
    [SerializeField] private int _currentAmmo; // current ammo in magazine
    [SerializeField] private int _backpackAmmo;
    [SerializeField] private bool _isFullauto = false;

    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
    public GameObject GunEnd { get => _gunEnd; set => _gunEnd = value; }
    public GameObject FireLight => _fireLight;
    public AudioClip SoundOfShoot => _soundOfShoot;
    public AudioClip SoundOfReload => _soundOfReload;
    public VisualEffect MuzzleFlash { get => _muzzleFlash; set => _muzzleFlash = value; }
    public float BulletForce { get => _bulletForce; set => _bulletForce = value; }
    public float ShootRate { get => _shootRate; set => _shootRate = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public int MagazineCapacity { get => _magazineCapacity; set => _magazineCapacity = value; }
    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public int BackpackAmmo { get => _backpackAmmo; set => _backpackAmmo = value; }
    public bool IsFullauto { get => _isFullauto; set => _isFullauto = value; }

    private void OnEnable()
    {
        OnWeaponEnable?.Invoke(this);
        IsCurrentlyUsed = true;
    }

    private void OnDisable()
    {
        IsCurrentlyUsed = false;
    }
}
