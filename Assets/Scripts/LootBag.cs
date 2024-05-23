using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBag : MonoBehaviour, IInteractable
{
    public List<ItemSO> items;
    public GameObject itemPrefab;
    public List<GameObject> itemObjects;

    public void Interact()
    {
        Debug.Log(items);
    }

    public void InteractionEnter(GameObject player)
    {
        if (InventoryManager.instance.isLooting)
        {
            return;
        }
        InventoryManager.instance.isLooting = true;
        List<Transform> children = new();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        int index = 0;
        for (var i = 0; i < children.Count; i++)
        {
            if (children[i].GetComponent<InventoryItem>())
            {
                children[i].gameObject.SetActive(true);
                children[i].SetParent(InventoryManager.instance.lootbagSlots[index].transform);
                index++;
            }
        }
        // InventoryManager.instance.SetItems(items);
        // InventoryManager.instance.isLooting = true;
        InventoryManager.instance.lootbagContainer.SetActive(true);
    }

    public void InteractionExit()
    {
        InventoryManager.instance.isLooting = false;
        InventoryManager.instance.HideLootbag(gameObject);
        // InventoryManager.instance.ClearItems();
        // InventoryManager.instance.isLooting = false;
        InventoryManager.instance.lootbagContainer.SetActive(false);
        if (transform.childCount == 1)
        {
            Destroy(gameObject);
        }
    }
}
