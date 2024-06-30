using UnityEngine;
using System.Collections;

public class MeleeWeaponController : MonoBehaviour, IAttackStrategy
{
    [SerializeField] private MeleeWeapon _meleeWeapon;
    [SerializeField] private float _animationDelay = 0.3f; // Adjust this value as needed
    [SerializeField] private float _soundDelay = 0.2f; // Adjust this value as needed

    private Camera _mainCamera;
    private AudioSource _audioSource;

    void Start()
    {
        _mainCamera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        if (!_meleeWeapon.IsAttacking)
        {
            _meleeWeapon.IsAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
        else if (_meleeWeapon.IsAttacking && !_meleeWeapon.IsChainAttack)
        {
            _meleeWeapon.IsChainAttack = true;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_animationDelay);
        _meleeWeapon.Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_soundDelay);
        _audioSource.PlayOneShot(_meleeWeapon.SoundOfAttack);

        RaycastMeleeAttack();
    }

    private void RaycastMeleeAttack()
    {
        Vector3 rayOrigin = _mainCamera.transform.position;
        Vector3 rayDirection = _mainCamera.transform.forward;
        float attackRange = _meleeWeapon.Range;

        Debug.DrawRay(rayOrigin, rayDirection * attackRange, Color.blue, 2.0f);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, attackRange))
        {
            if (hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
            {
                enemy.TakeDamage(_meleeWeapon.Dmg);
            }
        }
    }
}