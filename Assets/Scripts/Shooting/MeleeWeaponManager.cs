using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class MeleeWeaponManager : MonoBehaviour
{
    [SerializeField] private MeleeWeapon _meleeWeapon;

    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    private Camera _mainCamera;


    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].started += OnAttackTap;
        _mainCamera = Camera.main;
    }

    // Handle single mouse click
    private void OnAttackTap(InputAction.CallbackContext context)
    {
        if (_meleeWeapon.IsAttacking)
        {
            _meleeWeapon.IsChainAttack = true;
        }
        else
        {
            MeleeAttack();
        }
    }

    // Responsible for melee attack logic and animation
    private void MeleeAttack()
    {
        _meleeWeapon.IsAttacking = true;
        _meleeWeapon.Animator.SetTrigger("Attack");

        RaycastMeleeAtack();
    }

    // Check if the melee weapon hit the enemy
    private void RaycastMeleeAtack()
    {
        Vector3 rayOrigin = _mainCamera.transform.position;
        Vector3 rayDirection = _mainCamera.transform.forward;
        float attackRange = _meleeWeapon.Range;

        // Drawing the ray for checking range of melee weapon (only in scene view)
        Debug.DrawRay(rayOrigin, rayDirection * attackRange, Color.blue, 2.0f);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, attackRange))
        {
            // Check if the hit object has a health component or can be damaged
            if (hit.collider.TryGetComponent<EnemyTest>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(_meleeWeapon.Dmg);
            }
        }
    }
}