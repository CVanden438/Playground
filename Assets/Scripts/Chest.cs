using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public int slotCount;
    public GameObject itemPrefab;
    public GameObject slotPrefab;
    public List<GameObject> itemObjects;
    public GameObject chestUIContainer;
    public bool chestOpen;
    public Sprite closedChest;
    public Sprite openChest;
    SpriteRenderer sr;

    void Awake()
    {
        // chestUIContainer = GameObject.FindGameObjectWithTag("ChestUI");
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        if (!chestOpen)
        {
            sr.sprite = openChest;
            InventoryManager.instance.chestContainer.SetActive(true);
            foreach (Transform child in transform)
            {
                if (child.GetComponent<InventoryItem>())
                {
                    itemObjects.Add(child.gameObject);
                    child.gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < slotCount; i++)
            {
                Instantiate(slotPrefab, InventoryManager.instance.chestContainer.transform);
            }
            for (int i = 0; i < itemObjects.Count; i++)
            {
                itemObjects[i].transform.SetParent(
                    InventoryManager.instance.chestContainer.transform.GetChild(i)
                );
            }
            chestOpen = true;
            itemObjects.Clear();
        }
        else
        {
            sr.sprite = closedChest;
            InventoryManager.instance.HideChest(gameObject);
            chestOpen = false;
        }
    }

    public void InteractionEnter(GameObject player)
    {
        // throw new System.NotImplementedException();
    }

    public void InteractionExit()
    {
        if (chestOpen)
        {
            sr.sprite = closedChest;
            InventoryManager.instance.HideChest(gameObject);
            chestOpen = false;
        }
    }
}
