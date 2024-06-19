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
    }

    void Update()
    {
        if (_input.fire)
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        _fireaxe.Animator.SetTrigger("Attack");
    }
}
