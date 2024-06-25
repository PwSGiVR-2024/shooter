using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    private IAttackStrategy _currentAttackStrategy;

    // Singleton pattern
    public static AttackManager Instance { get; private set; }

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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAttackStrategy(GetComponent<RangeAttack>());
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAttackStrategy(GetComponent<MeleeAttack>());
        }
    }

    private void Start()
    {
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].started += AttackSingleClick;
        SetAttackStrategy(GetComponent<RangeAttack>());
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
        if (_currentAttackStrategy is RangeAttack rangeAttack && !rangeAttack.IsFullauto)
        {
            _currentAttackStrategy?.Attack();
        }
        else if (_currentAttackStrategy is MeleeAttack)
        {
            _currentAttackStrategy?.Attack();
        }
    }

    // Pressed attack for fullauto weapons
    private void AttackPressed()
    {
        if (_currentAttackStrategy is RangeAttack rangeAttack && rangeAttack.IsFullauto)
        {
            _currentAttackStrategy?.Attack();
        }
    }

    public void SetAttackStrategy(IAttackStrategy attackStrategy)
    {
        _currentAttackStrategy = attackStrategy;
    }
}
