using System;
using UnityEngine;

public class ItemContainerCallbacks : MonoBehaviour
{
    internal event Action<int> OnItemContainerCountChanged;
    private int _itemContainerCount = 0;

    public void OnItemContainerShow()
    {
        _itemContainerCount++;
        OnItemContainerCountChanged?.Invoke(_itemContainerCount);
    }

    public void OnItemContainerHide()
    {
        if (_itemContainerCount > 0)
        {
            _itemContainerCount--;
            OnItemContainerCountChanged?.Invoke(_itemContainerCount);
        }
    }

    public void OpenChestAnimate(string gameObjectName)
    {
        GameObject.Find(gameObjectName).GetComponent<Animator>().SetBool("Opened", true);
    }

    public void CloseChestAnimate(string gameObjectName)
    {
        GameObject.Find(gameObjectName).GetComponent<Animator>().SetBool("Opened", false);
    }
}
