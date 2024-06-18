using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

[ComponentMenu("Custom/On Open Window Action")]
[System.Serializable]
public class OnOpenWindowAction : Action
{
    [SerializeField] ItemContainer _itemContainer;

    public override ActionStatus OnUpdate()
    {
        if (_itemContainer.gameObject.activeSelf)
        {
            _itemContainer.Close();
            return ActionStatus.Failure;
        }
        else
        {
            _itemContainer.Show();
            return ActionStatus.Success;
        }
    }
}
