using DevionGames.InventorySystem;
using UnityEngine;

public class ItemContainerShortcuts : MonoBehaviour
{
    [SerializeField] string _itemContainerTag = "Item container";

    private GameObject[] _itemContainers;

    void Start()
    {
        _itemContainers = GameObject.FindGameObjectsWithTag(_itemContainerTag);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject itemContainerGo in _itemContainers)
            {
                ItemContainer itemContainer = itemContainerGo.GetComponent<ItemContainer>();
                itemContainer.Close();
            }
        }

    }

}