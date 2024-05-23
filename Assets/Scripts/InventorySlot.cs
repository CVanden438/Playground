using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool inventorySlot;
    public ItemType slotType;
    public EquipmentType slotEquipmentType;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        InventorySlot destinationSlot = this;

        if (slotType == ItemType.equipment)
        {
            var equipmentData = (EquipmentSO)draggedItem.data;
            if (equipmentData.equipmentType != slotEquipmentType)
            {
                return;
            }
            if (transform.childCount > 0)
            {
                InventoryManager.instance.RemoveStats(
                    (EquipmentSO)transform.GetChild(0).GetComponent<InventoryItem>().data
                );
            }
        }
        if (transform.childCount > 0)
        {
            // If the destination slot already has an item, swap places
            InventoryItem existingItem = transform.GetChild(0).GetComponent<InventoryItem>();
            existingItem.parentAfterDrag = draggedItem.parentAfterDrag;
            existingItem.transform.SetParent(draggedItem.parentAfterDrag);
            if (draggedItem.inInventory)
            {
                existingItem.inInventory = true;
            }
            else
            {
                existingItem.inInventory = false;
            }
        }

        // Update the parentAfterDrag of the dragged item
        draggedItem.parentAfterDrag = destinationSlot.transform;
        draggedItem.transform.SetParent(destinationSlot.transform);
        if (inventorySlot)
        {
            draggedItem.inInventory = true;
        }
        else
        {
            draggedItem.inInventory = false;
        }
    }
}
