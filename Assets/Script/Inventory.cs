using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : SingletonGameObject<Inventory>
{    
    private bool isActive = false;
    private InventorySlot[] m_slots;
    
    public InventorySlot[] Slots
    {
        get
        {
            if (m_slots == null) {
                m_slots = gameObject.GetComponentsInChildren<InventorySlot>(true);
            }
            return m_slots;
        }
    }
 
    public InventorySlot[] FindSlotsById(string id)
    {
        List<InventorySlot> list = new List<InventorySlot>();
        foreach (InventorySlot slot in Slots)
        {
            if (id.Equals(slot.Item.Id))
                list.Add(slot);
        }
        return list.ToArray();
    }
    
    public InventorySlot FindSlotById(string id)
    {
        InventorySlot inventorySlot = null;
        foreach (InventorySlot slot in Slots)
        {
            if (slot.Item != null && id.Equals(slot.Item.Id)) {
                inventorySlot = slot;            
            }
        }
        return inventorySlot;        
    }
    
    public InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.Quantity == 0)
            {
                return slot;
            }
        }
        return null;        
    }

    public bool AddItem(Item item, int quantity = 1)
    {
        InventorySlot inventorySlot = FindSlotById(item.Id);
        if (inventorySlot != null)
        {
            Debug.Log("Find matching slot" + inventorySlot);
            inventorySlot.Quantity = inventorySlot.Quantity + quantity;
            return true;
        } 
        else
        {
            inventorySlot = FindNextEmptySlot();
            Debug.Log("No matching slot, use empty slot " + inventorySlot.ToString());
            if(inventorySlot != null) {
                inventorySlot.Item = new Item(item);
                inventorySlot.Quantity = quantity;
                Debug.Log("Item added. " );
                return true;
            } else {
                return false;
            }
        }
    }
    
    public bool RemoveItem(Item item, int quantity = 1)
    {
        InventorySlot inventorySlot = FindSlotById(item.Id);
        if (inventorySlot != null)
        {
            int newQuantity = inventorySlot.Quantity - quantity;
            if(newQuantity <= 0) {
                inventorySlot.Item = null;
                inventorySlot.Quantity = 0;
            } else {
                inventorySlot.Quantity = newQuantity;
            }
            return true;
        } 
        return false;
    }
        
    public void ToggleInventory()
    {
        if (!isActive)
        {
            gameObject.SetActive(true);
            isActive = true;
            
            Tailoring.instance.Close();
        } else
        {
            gameObject.SetActive(false);
            isActive = false;
        }
    }
    
    public void Close() {
        gameObject.SetActive(false);
        isActive = false;
    }        


}
