using UnityEngine;

public class MeleeWeaponController : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private MeleeWeapon _meleeWeapon;

    private Camera _mainCamera;


    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void Attack()
    {
        // Handle attack animation logic
        if (!_meleeWeapon.IsAttacking)
        {
            _meleeWeapon.IsAttacking = true;
            _meleeWeapon.Animator.SetTrigger("Attack");
            RaycastMeleeAtack();
        }
        else if (_meleeWeapon.IsAttacking && !_meleeWeapon.IsChainAttack)
        {
            _meleeWeapon.IsChainAttack = true;
        }

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
            if (hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
            {
                enemy.TakeDamage(_meleeWeapon.Dmg);
            }
        }
    }
}