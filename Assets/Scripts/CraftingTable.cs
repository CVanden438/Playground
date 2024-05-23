using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public GameObject craftingWindow;
    public GameObject recipieContainer;
    public GameObject recipieItem;
    public GameObject craftButton;
    bool craftingOpen = false;
    public List<RecipieSO> recipies = new();
    List<RecipieSO> craftableRecipies = new();

    void GetCraftableRecipies()
    {
        craftableRecipies.Clear();
        foreach (var recipie in recipies)
        {
            bool canCraft = true;
            foreach (var item in recipie.inputItems)
            {
                if (InventoryManager.instance.CountItem(item.item) < item.quantity)
                {
                    canCraft = false;
                }
            }
            if (canCraft)
            {
                craftableRecipies.Add(recipie);
            }
        }
    }

    public void ShowCraftingUI(List<RecipieSO> recipies)
    {
        craftingWindow.SetActive(true);
        foreach (var recipie in recipies)
        {
            var container = Instantiate(recipieContainer, craftingWindow.transform);
            var outputItem = Instantiate(recipieItem, container.transform);
            outputItem.GetComponent<Image>().sprite = recipie.outputItem.sprite;
            foreach (var item in recipie.inputItems)
            {
                for (var i = 0; i < item.quantity; i++)
                {
                    var inputItem = Instantiate(recipieItem, container.transform);
                    inputItem.GetComponent<Image>().sprite = item.item.sprite;
                }
            }
            var button = Instantiate(craftButton, container.transform);
            button.GetComponent<Button>().onClick.AddListener(() => Craft(recipie));
        }
    }

    public void HideCraftingUI()
    {
        foreach (Transform child in craftingWindow.transform)
        {
            Destroy(child.gameObject);
        }
        craftingWindow.SetActive(false);
        craftingOpen = false;
    }

    void Craft(RecipieSO recipie)
    {
        foreach (var inputItem in recipie.inputItems)
        {
            Debug.Log("Quantity" + inputItem.quantity);
            for (int i = 0; i < inputItem.quantity; i++)
            {
                InventoryManager.instance.DestroyInventoryItem(inputItem.item);
            }
        }

        var freeSlotIndex = InventoryManager.instance.GetFirstFreeSlotIndexInventory();
        if (freeSlotIndex != -1)
        {
            Debug.Log("FREE SLOT" + freeSlotIndex);
            var craftedItem = Instantiate(
                InventoryManager.instance.itemPrefab,
                InventoryManager.instance.inventorySlots[freeSlotIndex].transform
            );
            craftedItem.GetComponent<InventoryItem>().data = recipie.outputItem;
            craftedItem.GetComponent<InventoryItem>().inInventory = true;
        }
        else
        {
            Debug.LogWarning("No free slot available for crafted item.");
        }
        HideCraftingUI();
        Interact();
    }

    public void Interact()
    {
        if (!craftingOpen)
        {
            craftingOpen = true;
            GetCraftableRecipies();
            ShowCraftingUI(craftableRecipies);
        }
        else
        {
            HideCraftingUI();
        }
    }

    public void InteractionEnter(GameObject player) { }

    public void InteractionExit()
    {
        craftingOpen = false;
        HideCraftingUI();
    }
}
