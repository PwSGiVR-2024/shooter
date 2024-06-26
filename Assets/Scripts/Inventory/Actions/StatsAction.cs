﻿using DevionGames;
using StarterAssets;
using UnityEngine;

[ComponentMenu("Custom/Stats Action")]
[System.Serializable]
public class StatsAction : Action
{
    public enum ActionType
    {
        Speed, Jump, Power, Protection, Ghoul, Magazine, FuriousGhoul
    }

    [SerializeField] int _changePercentage = 10;
    [SerializeField] ActionType _actionType;

    private GameObject _target;
    private PlayerHealth _playerHealth;
    private FirstPersonController _playerController;

    public override void OnStart()
    {
        _target = GameObject.Find("PlayerBody");
        _playerHealth = _target.GetComponent<PlayerHealth>();
        _target = GameObject.Find("PlayerCapsule");
        _playerController = _target.GetComponent<FirstPersonController>();
    }

    public override ActionStatus OnUpdate()
    {
        if (_actionType == ActionType.Speed)
        {
            _playerController.MoveSpeed += _playerController.MoveSpeed * _changePercentage / 100;
            _playerController.SprintSpeed += _playerController.SprintSpeed * _changePercentage / 100;
            _playerController.CrouchSpeed += _playerController.CrouchSpeed * _changePercentage / 100;
        }
        else if (_actionType == ActionType.Jump)
        {
            _playerController.JumpHeight += _playerController.JumpHeight * _changePercentage / 100;
        }
        else if (_actionType == ActionType.Power)
        {
            //after merge
        }
        else if (_actionType == ActionType.Protection)
        {
            _playerHealth.ProtectionPercentage += _changePercentage;
        }
        else if (_actionType == ActionType.Ghoul)
        {
            System.Random random = new System.Random();
            int effectType = random.Next(4);
            bool increase = random.Next(2) == 0;

            float changeFactor = increase ? (1 + _changePercentage / 100f) : (1 - _changePercentage / 100f);

            switch (effectType)
            {
                case 0:
                    _playerController.MoveSpeed *= changeFactor;
                    _playerController.SprintSpeed *= changeFactor;
                    _playerController.CrouchSpeed *= changeFactor;
                    break;
                case 1:
                    _playerController.JumpHeight *= changeFactor;
                    break;
                case 2:
                    //after merge
                    break;
                case 3:
                    _playerHealth.ProtectionPercentage = increase ? _playerHealth.ProtectionPercentage + _changePercentage : _playerHealth.ProtectionPercentage - _changePercentage;
                    break;
            }
        }
        else if (_actionType == ActionType.Magazine)
        {
            //after marge
        }
        else if (_actionType == ActionType.FuriousGhoul)
        {
            FuriousGhoulCoroutine furiousGhoulCoroutine = gameObject.AddComponent<FuriousGhoulCoroutine>();
            furiousGhoulCoroutine.StartTheCoroutine(_changePercentage, _playerController, _playerHealth);
        }
        return ActionStatus.Success;
    }
}
