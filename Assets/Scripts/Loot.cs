using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Loot : MonoBehaviour
{
    DropTableSO dropTable;
    List<ItemSO> droppedItems = new();
    public GameObject lootbagPrefab;
    public GameObject itemPrefab;
    Health health;

    void Awake()
    {
        dropTable = GetComponent<Enemy>().data.dropTable;
        health = GetComponent<Health>();
        health.onDeath.AddListener(GenerateDrops);
    }

    public void GenerateDrops()
    {
        foreach (var item in dropTable.drops)
        {
            var randomNum = Random.Range(0, 100);
            if (randomNum < item.chance)
            {
                droppedItems.Add(item.item);
            }
        }
        if (droppedItems.Count > 0)
        {
            var lootbag = Instantiate(lootbagPrefab, transform.position, Quaternion.identity);
            // var lootbagItems = lootbag.GetComponent<LootBag>();
            foreach (var item in droppedItems)
            {
                // lootbagItems.items.Add(item);
                var newItem = Instantiate(itemPrefab, lootbag.transform);
                newItem.GetComponent<InventoryItem>().data = item;
                newItem.SetActive(false);
            }
        }
    }
}
