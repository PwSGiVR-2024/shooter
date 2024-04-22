using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public event Action<Weapon> OnWeaponEnable;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _gunEnd;
    [SerializeField] private AudioClip _soundOfShoot;
    [SerializeField] private AudioClip _soundOfReload;
    [SerializeField] private float _bulletForce = 10f;
    [SerializeField] private float _dmg = 5f;
    [SerializeField] private float _shootRate = 0.5f; // interval between shots in seconds
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private int _magazineCapacity = 30;
    [SerializeField] private int _currentAmmo; // current ammo in magazine
    [SerializeField] private int _backpackAmmo;
    //[SerializeField] private bool _isFullAuto = false;
    //[SerializeField] private bool _isShotgun = false;
    private bool _isCurrentlyUsed = false;
    private Animator _animator;

    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
    public GameObject GunEnd { get => _gunEnd; set => _gunEnd = value; }
    public AudioClip SoundOfShoot => _soundOfShoot;
    public AudioClip SoundOfReload => _soundOfReload;
    public float BulletForce { get => _bulletForce; set => _bulletForce = value; }
    public float Dmg { get => _dmg; set => _dmg = value; }
    public float ShootRate { get => _shootRate; set => _shootRate = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public int MagazineCapacity { get => _magazineCapacity; set => _magazineCapacity = value; }
    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public bool IsCurrentlyUsed => _isCurrentlyUsed;
    public int BackpackAmmo { get => _backpackAmmo; set => _backpackAmmo = value; }
    public Animator Animator { get => _animator; set => _animator = value; }

    private void OnEnable()
    {
        OnWeaponEnable?.Invoke(this);
        _isCurrentlyUsed = true;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _isCurrentlyUsed = false;
    }

}
