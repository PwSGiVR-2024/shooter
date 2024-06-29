using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action<Weapon> OnWeaponChange;

    [SerializeField] private int _dmg;
    [SerializeField] private bool _isUnlocked = false;

    private Animator _animator;
    private bool _isCurrentlyUsed = false;

    public int Dmg { get => _dmg; set => _dmg = value; }
    public Animator Animator => _animator;
    public bool IsCurrentlyUsed { get => _isCurrentlyUsed; set => _isCurrentlyUsed = value; }
    public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }

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
