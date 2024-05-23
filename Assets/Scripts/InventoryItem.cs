using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem
    : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerUpHandler
{
    public ItemSO data;
    Image image;

    public bool inInventory;

    [HideInInspector]
    public Transform parentAfterDrag;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.sprite = data.sprite;
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (InventoryManager.instance.isDragging)
        {
            return;
        }
        if (!inInventory)
        {
            var slotType = GetComponentInParent<InventorySlot>().slotType;
            if (slotType == ItemType.equipment && data is EquipmentSO equipment)
            {
                InventoryManager.instance.RemoveStats(equipment);
            }
            InventoryManager.instance.TransferItemToInventory(gameObject);
        }
        else
        {
            InventoryManager.instance.TransferItemFromInventory(gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotType = GetComponentInParent<InventorySlot>().slotType;
        if (slotType == ItemType.equipment && data is EquipmentSO equipment)
        {
            InventoryManager.instance.RemoveStats(equipment);
        }
        InventoryManager.instance.isDragging = true;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryManager.instance.isDragging = false;
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        var slotType = GetComponentInParent<InventorySlot>().slotType;
        if (slotType == ItemType.equipment && data is EquipmentSO equipment)
        {
            InventoryManager.instance.ApplyStats(equipment);
        }
    }
}
