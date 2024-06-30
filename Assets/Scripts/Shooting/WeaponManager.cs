using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject _weaponsSlot;
    [SerializeField] Weapon _startWeapon;

    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    private IAttackStrategy _currentAttackStrategy;
    private Weapon _currentWeapon;
    private ItemContainerCallbacks _itemContainerCallbacks;
    private int _itemContainerCount;
    private Dictionary<string, Weapon> _weapons = new Dictionary<string, Weapon>();

    public Weapon CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    // Singleton pattern
    public static WeaponManager Instance { get; private set; }

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

    public void Update()
    {
        HandleAttackPressed();
    }

    private void Start()
    {
        // Subscribe to weapon change event
        Weapon[] weapons = _weaponsSlot.GetComponentsInChildren<Weapon>(true);
        foreach (var weapon in weapons)
        {
            _weapons.Add(weapon.name, weapon);
            weapon.OnWeaponChange += GetCurrentWeaponData;
        }

        // Setting start weapon
        if (_startWeapon != null)
        {
            _currentWeapon = _startWeapon;
            WeaponSwitcher.Instance.SetWeaponActive(_currentWeapon);
            UIController.Instance.ShowAmmoUI(_currentWeapon is RangeWeapon);
        }
        else
        {
            UIController.Instance.ShowAmmoUI(false);
        } 

        // Handle input
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].started += AttackSingleClick;

        // Handle container count
        _itemContainerCallbacks = GameObject.Find("ItemContainerMethods").GetComponent<ItemContainerCallbacks>();
        _itemContainerCallbacks.OnItemContainerCountChanged += UpdateItemContainerCount;
    }

    public void GetCurrentWeaponData(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void UnlockWeapon(string weaponName)
    {
        if (_weapons.ContainsKey(weaponName))
        {
            Debug.Log($"Weapon {weaponName} has been unlocked");
            _weapons[weaponName].IsUnlocked = true;
            WeaponSwitcher.Instance.SetWeaponActive(_weapons[weaponName]);
        }
    }

    private void HandleAttackPressed()
    {
        // If there is no item container and fire button is pressed (e.g. crafting, inventory, menu)
        if (_itemContainerCount == 0 && _input.fire)
        {
            AttackPressed();
        }
    }

    // Handle single tap for melee weapons and not fullauto range weapons
    private void AttackSingleClick(InputAction.CallbackContext context)
    {
        if (_itemContainerCount != 0)
        {
            return;
        }
        if (_currentAttackStrategy is RangeWeaponController rangeAttack && !rangeAttack.IsFullauto)
        {
            _currentAttackStrategy?.Attack();
        }
        else if (_currentAttackStrategy is MeleeWeaponController)
        {
            _currentAttackStrategy?.Attack();
        }
    }

    // Pressed attack for fullauto weapons
    private void AttackPressed()
    {
        if (_currentAttackStrategy is RangeWeaponController rangeAttack && rangeAttack.IsFullauto)
        {
            _currentAttackStrategy?.Attack();
        }
    }

    public void SetAttackStrategy(IAttackStrategy attackStrategy)
    {
        _currentAttackStrategy = attackStrategy;
    }

    private void UpdateItemContainerCount(int itemContainerCount)
    {
        _itemContainerCount = itemContainerCount;
    }
}
