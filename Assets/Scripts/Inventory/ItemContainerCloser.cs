using DevionGames.InventorySystem;
using UnityEngine;

public class ItemContainerCloser : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ItemContainer container = GameObject.Find("Crafting").GetComponent<ItemContainer>();
            if (container.IsVisible)
            {
                container.Close();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject shopContainerGo = GameObject.Find("Vendor");
            if (shopContainerGo != null)
            {
                ItemContainer shopContainer = shopContainerGo.GetComponent<ItemContainer>();
                if (shopContainer.IsVisible)
                {
                    shopContainer.Close();
                }
            }
            else
            {
                GameObject lootContainerGo = GameObject.Find("Chest");
                if (lootContainerGo != null)
                {
                    ItemContainer lootContainer = lootContainerGo.GetComponent<ItemContainer>();
                    if (lootContainer != null && lootContainer.IsVisible)
                    {
                        lootContainer.Close();
                    }
                }
            }
        }
    }
}