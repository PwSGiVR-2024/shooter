using System.Collections;
using StarterAssets;
using UnityEngine;

public class FuriousGhoulCoroutine : MonoBehaviour
{
    public void StartTheCoroutine(int _changePercentage, StarterAssets.FirstPersonController _playerController, PlayerHealth _playerHealth)
    {
        StartCoroutine(ApplyFuriousGhoulEffect(_changePercentage, _playerController, _playerHealth));
    }

    private IEnumerator ApplyFuriousGhoulEffect(int _changePercentage, FirstPersonController _playerController, PlayerHealth _playerHealth)
    {
        float changeFactor = 1 + _changePercentage / 100f; // Calculate change factor based on _changePercentage

        float originalMoveSpeed = _playerController.MoveSpeed;
        float originalSprintSpeed = _playerController.SprintSpeed;
        //after merge

        _playerController.MoveSpeed *= changeFactor;
        _playerController.SprintSpeed *= changeFactor;
        //after merge

        yield return new WaitForSeconds(120);

        _playerController.MoveSpeed = originalMoveSpeed;
        _playerController.SprintSpeed = originalSprintSpeed;
        //after merge
        int protectionPercentage = _playerHealth.ProtectionPercentage;
        _playerHealth.ProtectionPercentage = 0;
        _playerHealth.CurrentHealth = 1;
        _playerHealth.ProtectionPercentage = protectionPercentage;
    }
}
