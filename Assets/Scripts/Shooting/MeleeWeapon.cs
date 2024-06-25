using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _range;

    private bool _isAttacking = false;
    private bool _isChainAttack = false;

    public float Range { get => _range; set => _range = value; }

    public bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            _isAttacking = value;
            Animator.SetBool("isAttacking", value);
        }
    }
    public bool IsChainAttack
    {
        get => _isChainAttack;
        set
        {
            _isChainAttack = value;
            Animator.SetBool("isChainAttack", value);
        }
    }


    // Those events are called at the end of the attack animation in animation clip
    public void OnAttack1End()
    {
        IsAttacking = false;
    }

    public void OnAttack2End()
    {
        IsAttacking = false;
        IsChainAttack = false;
    }

}
