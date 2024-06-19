using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private Animator _animator;
    private float _dmg;
    private float _range;

    public Animator Animator => _animator;
    public float Dmg { get => _dmg; set => _dmg = value; }
    public float Range { get => _range; set => _range = value; }


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
