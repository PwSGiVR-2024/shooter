using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class MeleeWeaponManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;
    [SerializeField] private MeleeWeapon _fireaxe;

    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].started += OnAttackTap;
    }

    private void OnAttackTap(InputAction.CallbackContext context)
    {
        if (_fireaxe.IsAttacking)
        {
            _fireaxe.IsChainAttack = true;
        }
        else
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        _fireaxe.IsAttacking = true;
        _fireaxe.Animator.SetTrigger("Attack");
    }
}