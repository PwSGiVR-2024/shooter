using System.Collections;
using StarterAssets;
using UnityEngine;

public class FuriousGhoulCoroutine : MonoBehaviour
{
    RangeWeaponController _rangeWeaponController;

    public void StartTheCoroutine(int _changePercentage, StarterAssets.FirstPersonController _playerController, PlayerHealth _playerHealth, Weapon[] weapons, RangeWeaponController rangeWeaponController)
    {
        _rangeWeaponController = rangeWeaponController;
        StartCoroutine(ApplyFuriousGhoulEffect(_changePercentage, _playerController, _playerHealth, weapons));
    }

    private IEnumerator ApplyFuriousGhoulEffect(int _changePercentage, FirstPersonController _playerController, PlayerHealth _playerHealth, Weapon[] weapons)
    {
        float changeFactor = 1 + _changePercentage / 100f; // Calculate change factor based on _changePercentage

        float originalMoveSpeed = _playerController.MoveSpeed;
        float originalSprintSpeed = _playerController.SprintSpeed;
        float originalCrouchSpeed = _playerController.CrouchSpeed;
        // original weapon damages
        float[] originalWeaponDamages = new float[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            originalWeaponDamages[i] = weapons[i].Dmg;
        }


        _playerController.MoveSpeed *= changeFactor;
        _playerController.SprintSpeed *= changeFactor;
        _playerController.CrouchSpeed *= changeFactor;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Dmg *= changeFactor;
            TryRefresh(weapons[i]);
        }

        yield return new WaitForSeconds(60);
        if (_playerHealth.CurrentHealth >= 1)
        {
            _playerController.MoveSpeed = originalMoveSpeed;
            _playerController.SprintSpeed = originalSprintSpeed;
            _playerController.CrouchSpeed = originalCrouchSpeed;
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].Dmg = originalWeaponDamages[i];
                TryRefresh(weapons[i]);
            }
            int protectionPercentage = _playerHealth.ProtectionPercentage;
            _playerHealth.ProtectionPercentage = 0;
            _playerHealth.CurrentHealth = 1;
            _playerHealth.ProtectionPercentage = protectionPercentage;
        }
    }

    private void TryRefresh(Weapon weapon)
    {
        if (weapon.gameObject.activeSelf)
        {
            _rangeWeaponController.GetCurrentWeaponData(weapon);
        }
    }
}
