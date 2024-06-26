using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject _weaponsSlot;
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    private IAttackStrategy _currentAttackStrategy;

    private Weapon _currentWeapon;

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
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

        Weapon[] weapons = _weaponsSlot.GetComponentsInChildren<Weapon>(true);
        foreach (var weapon in weapons)
        {
            weapon.OnWeaponChange += GetCurrentWeaponData;
        }

        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].started += AttackSingleClick;
        SetAttackStrategy(GetComponent<RangeWeaponController>());
    }

    public void GetCurrentWeaponData(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void HandleAttackPressed()
    {
        if (_input.fire)
        {
            AttackPressed();
        }
    }

    // Handle single tap for melee weapons and not fullauto range weapons
    private void AttackSingleClick(InputAction.CallbackContext context)
    {
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
}
