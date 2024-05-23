using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public TextMeshProUGUI defText;
    public PlayerStats playerStats;
    public GameObject itemPrefab;
    public MeleeSwing attack;
    public List<GameObject> createdItems;
    public bool isLooting;
    public bool isDragging;

    [Header("Inventory")]
    public List<GameObject> inventorySlots;
    public List<ItemSO> inventoryItems;

    [Header("Lootbag")]
    public List<GameObject> lootbagSlots;
    public List<ItemSO> lootbagItems;
    public GameObject lootbagContainer;

    [Header("Chest")]
    public GameObject chestContainer;

    [Header("Equipment")]
    public GameObject helmSlot;
    public GameObject chestSlot;
    public GameObject legSlot;
    public GameObject bootSlot;
    public GameObject wepSlot;
    public GameObject spellSlot;
    public GameObject jewellerySlot1;
    public GameObject jewellerySlot2;

    [Header("Crafting")]
    public GameObject craftingWindow;
    public GameObject recipieContainer;
    public GameObject recipieItem;
    public GameObject craftButton;

    void Awake()
    {
        instance = this;
    }

    public void HideLootbag(GameObject gameObject)
    {
        for (var i = 0; i < lootbagSlots.Count; i++)
        {
            if (lootbagSlots[i].transform.childCount != 0)
            {
                lootbagSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                lootbagSlots[i].transform.GetChild(0).SetParent(gameObject.transform);
            }
        }
    }

    public void HideChest(GameObject gameObject)
    {
        chestContainer.SetActive(false);
        for (var i = 0; i < chestContainer.transform.childCount; i++)
        {
            if (chestContainer.transform.GetChild(i).childCount != 0)
            {
                chestContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                chestContainer.transform.GetChild(i).GetChild(0).SetParent(gameObject.transform);
            }
        }
        for (var i = 0; i < chestContainer.transform.childCount; i++)
        {
            Destroy(chestContainer.transform.GetChild(i).gameObject);
        }
    }

    public void TransferItemToInventory(GameObject gameObject)
    {
        int firstFreeSlot = GetFirstFreeSlotIndexInventory();
        if (firstFreeSlot != -1)
        {
            gameObject.GetComponent<InventoryItem>().inInventory = true;
            gameObject.transform.SetParent(inventorySlots[firstFreeSlot].transform);
        }
    }

    public void TransferItemFromInventory(GameObject gameObject)
    {
        int firstFreeSlot;
        if (chestContainer.activeInHierarchy)
        {
            firstFreeSlot = GetFirstFreeSlotIndexChest();
            if (firstFreeSlot != -1)
            {
                gameObject.transform.SetParent(
                    chestContainer.transform.GetChild(firstFreeSlot).transform
                );
                gameObject.GetComponent<InventoryItem>().inInventory = false;
            }
            return;
        }
        if (lootbagContainer.activeInHierarchy)
        {
            firstFreeSlot = GetFirstFreeSlotIndexLootbag();
            if (firstFreeSlot != -1)
            {
                gameObject.transform.SetParent(lootbagSlots[firstFreeSlot].transform);
                gameObject.GetComponent<InventoryItem>().inInventory = false;
            }
            return;
        }
        var itemData = gameObject.GetComponent<InventoryItem>().data;
        if (itemData.itemType == ItemType.equipment)
        {
            var equipmentData = (EquipmentSO)itemData;
            var equipmentType = equipmentData.equipmentType;
            // ApplyStats(equipmentData);
            switch (equipmentType)
            {
                case EquipmentType.helm:
                    var helmHasChild = helmSlot.transform.childCount;
                    gameObject.transform.SetParent(helmSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (helmHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            helmSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(helmSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.chest:
                    var chestHasChild = chestSlot.transform.childCount;
                    gameObject.transform.SetParent(chestSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (chestHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            chestSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(chestSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.legs:
                    var legsHasChild = legSlot.transform.childCount;
                    gameObject.transform.SetParent(legSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (legsHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            legSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(legSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.boots:
                    var bootsHasChild = bootSlot.transform.childCount;
                    gameObject.transform.SetParent(bootSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (bootsHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            bootSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(bootSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.jewellery:
                    var jewelleryHasChild = jewellerySlot1.transform.childCount;
                    gameObject.transform.SetParent(jewellerySlot1.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (jewelleryHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            jewellerySlot1.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(jewellerySlot1.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.spell:
                    var spellHasChild = spellSlot.transform.childCount;
                    gameObject.transform.SetParent(spellSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (spellHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            spellSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(spellSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
                case EquipmentType.weapon:
                    var weaponHasChild = wepSlot.transform.childCount;
                    gameObject.transform.SetParent(wepSlot.transform);
                    gameObject.GetComponent<InventoryItem>().inInventory = false;
                    if (weaponHasChild == 1)
                    {
                        var equippedStats = (EquipmentSO)
                            wepSlot.transform.GetChild(0).GetComponent<InventoryItem>().data;
                        TransferItemToInventory(wepSlot.transform.GetChild(0).gameObject);
                        RemoveStats(equippedStats);
                    }
                    break;
            }
            ApplyStats(equipmentData);
        }
    }

    public int GetFirstFreeSlotIndexInventory()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].transform.childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetFirstFreeSlotIndexLootbag()
    {
        for (int i = 0; i < lootbagSlots.Count; i++)
        {
            if (lootbagSlots[i].transform.childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetFirstFreeSlotIndexChest()
    {
        for (int i = 0; i < chestContainer.transform.childCount; i++)
        {
            if (chestContainer.transform.GetChild(i).childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

    public int CountItem(ItemSO item)
    {
        inventoryItems.Clear();
        foreach (var slot in inventorySlots)
        {
            if (slot.transform.childCount == 1)
            {
                inventoryItems.Add(slot.transform.GetChild(0).GetComponent<InventoryItem>().data);
            }
        }
        int count = 0;
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem == item)
            {
                count++;
            }
        }
        Debug.Log("Count" + item + count);
        return count;
    }

    public void DestroyInventoryItem(ItemSO item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.transform.childCount == 1)
            {
                var inventoryItem = slot.transform.GetChild(0).GetComponent<InventoryItem>();
                if (inventoryItem.data == item)
                {
                    Destroy(slot.transform.GetChild(0).gameObject);
                    //destory gets called end of frame so do this to psudo destory it until then
                    slot.transform.DetachChildren();
                    break;
                }
            }
        }
    }

    public void ApplyStats(EquipmentSO item)
    {
        if (item is WeaponSO wep)
        {
            attack.wepData = wep;
        }
        var stats = item.stats;
        foreach (var playerStat in playerStats.stats)
        {
            if (item is ArmourSO armour)
            {
                if (playerStat.statType == StatType.defence)
                {
                    playerStat.AddModifier(armour.baseDefence);
                }
            }
            if (item is BootSO boot)
            {
                if (playerStat.statType == StatType.movespeed)
                {
                    playerStat.AddModifier(boot.baseMovespeed);
                }
            }
            foreach (var stat in stats)
            {
                if (stat.type == playerStat.statType)
                {
                    playerStat.AddModifier(stat.value);
                }
            }
        }
        UpdateDefence();
    }

    public void RemoveStats(EquipmentSO item)
    {
        if (item is WeaponSO wep)
        {
            attack.wepData = null;
            attack.meleeWeaponPivot.SetActive(false);
            attack.rangedWeapon.SetActive(false);
        }
        var stats = item.stats;
        foreach (var playerStat in playerStats.stats)
        {
            if (item is ArmourSO armour)
            {
                if (playerStat.statType == StatType.defence)
                {
                    playerStat.RemoveAddModifier(armour.baseDefence);
                }
            }
            if (item is BootSO boot)
            {
                if (playerStat.statType == StatType.movespeed)
                {
                    playerStat.RemoveAddModifier(boot.baseMovespeed);
                }
            }
            foreach (var stat in stats)
            {
                if (stat.type == playerStat.statType)
                {
                    playerStat.RemoveAddModifier(stat.value);
                }
            }
        }
        UpdateDefence();
    }

    public void UpdateStats() { }

    public void UpdateDefence()
    {
        defText.text = playerStats.stats
            .Find(x => x.statType == StatType.defence)
            .GetFinalValue()
            .ToString();
        Debug.Log("Cliekd");
    }
}
