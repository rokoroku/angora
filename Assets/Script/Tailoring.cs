using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tailoring : SingletonGameObject<Tailoring> {
    
    private List<Sketch> m_availableSketch;

    private List<TailoringSlot> m_slots;    
    private bool isActive = false;
    
    public List<TailoringSlot> SlotList
    {
        get
        {
            if (m_slots == null) {
                m_slots = new List<TailoringSlot>(GetComponentsInChildren<TailoringSlot>(true));
            }
            return m_slots;
        }
    }
    
    List<Sketch> AvailableSketchList {
        get {
            if(m_availableSketch == null) m_availableSketch = new List<Sketch>();
            return this.m_availableSketch;
        }
    }
    
    public TailoringSlot FindNextEmptySlot()
    {
        foreach (TailoringSlot slot in SlotList)
        {
            if (slot.Sketch == null)
            {
                return slot;
            }
        }
        return null;        
    }
    
    public bool AddItem(Sketch item)
    {
        TailoringSlot tailoringSlot = FindNextEmptySlot();
        if(tailoringSlot != null) {
            tailoringSlot.SetItem(item);
            return true;
        } else {
            return false;
        }
    }
    
    public bool RemoveItem(TailoringSlot slot)
    {
        if(SlotList.Contains(slot)) {
            slot.Clear();
            return true;
        } 
        return false;
    }
    
    public void ToggleTailoring()
    {
        if (!isActive)
        {
            gameObject.SetActive(true);
            isActive = true;
            Inventory.instance.Close();
            
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
    
    void Update() {
        foreach(TailoringSlot slot in SlotList) {
            if(slot.Sketch != null && slot.RemainingTime <= 0) {
                Item item = Item.FromId(slot.Sketch.ItemId);
                Debug.Log("slot" + slot + " finished the job. item " + item + " will be added to your inventory.");
                if(item != null) {
                    Inventory.instance.AddItem(item);
                }
                slot.Clear();
            }
        }
    }
}
