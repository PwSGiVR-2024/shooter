using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action<Weapon> OnWeaponChange;

    [SerializeField] private float _dmg;

    private Animator _animator;
    private bool _isCurrentlyUsed = false;

    public Animator Animator => _animator;
    public float Dmg { get => _dmg; set => _dmg = value; }
    public bool IsCurrentlyUsed { get => _isCurrentlyUsed; set => _isCurrentlyUsed = value; }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnWeaponChange?.Invoke(this);
        IsCurrentlyUsed = true;
    }

    private void OnDisable()
    {
        IsCurrentlyUsed = false;
    }
}
