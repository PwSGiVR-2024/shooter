using DevionGames;
using UnityEngine;

[ComponentMenu("Custom/Weapon Unlock Action")]
[System.Serializable]
public class WeaponUnlock : Action
{
    [SerializeField] string _weaponName;

    public override ActionStatus OnUpdate()
    {
        WeaponManager.Instance.UnlockWeapon(_weaponName);
        return ActionStatus.Success;
    }
}
