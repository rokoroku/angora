using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SketchSlot : MonoBehaviour, IDialog {

    [SerializeField] private Image m_image;
    [SerializeField] private Sketch m_sketch;
    
    [SerializeField] private string m_id;
    [SerializeField] private string m_itemId;
    [SerializeField] private int m_tier;
    [SerializeField] private int m_price;
    [SerializeField] private float m_requiredTime;
    
    [SerializeField] private string m_requireItem1Id;
    [SerializeField] private int m_requireItem1quantity;
    [SerializeField] private string m_requireItem2Id;
    [SerializeField] private int m_requireItem2quantity;

    Sketch Sketch {
        get {
            if(m_sketch == null) {
                List<Sketch.Requirement> requirements = new List<Sketch.Requirement>();
                if(m_requireItem1quantity > 0) {
                    requirements.Add(new Sketch.Requirement(m_requireItem1Id, m_requireItem1quantity));
                }
                if(m_requireItem2quantity > 0) {
                    requirements.Add(new Sketch.Requirement(m_requireItem2Id, m_requireItem2quantity));
                }
                m_sketch = new Sketch(m_id, m_itemId, m_tier, m_price, m_requiredTime, requirements);
            }
            return m_sketch;         
        }
        set {
            m_sketch = value;
        }
    }
    
    void Start() {
        if(m_itemId != null) {
            ImageUtil.ChangeSprite(m_image.gameObject, Item.FromId(m_itemId).Sprite);
            m_image.enabled = true;
        } else {
            m_image.enabled = false;
        }
    }
    
    public void OnClick() {
        TailoringDialog dialog = new TailoringDialog.Builder().SetSketch(Sketch).SetDialogInteractionListener(this).Build();
    }
    
    public void OnPositiveClick ()
    {
        bool possible = true;
        foreach(Sketch.Requirement requirement in Sketch.Requirements) {
            InventorySlot inventorySlot = Inventory.instance.FindSlotById(requirement.ItemId);
            if(requirement.Quantity == 0) {
                continue;
            }
            if(inventorySlot != null && inventorySlot.Quantity >= requirement.Quantity) {
                continue;
            }
            possible = false;
            break;
        }
        if(possible) {
            foreach(Sketch.Requirement requirement in Sketch.Requirements) {
                InventorySlot inventorySlot = Inventory.instance.FindSlotById(requirement.ItemId);
                inventorySlot.Quantity = inventorySlot.Quantity - requirement.Quantity;
            }
            Tailoring.instance.AddItem(Sketch);
        } else {
            var dialog = new ConfirmDialog.Builder().setTitle("Error").setContent("You don't have enough hairs").Build() as ConfirmDialog;        
        }
    }

    public void OnNegativeClick ()
    {
        
    }

    public void OnCancel ()
    {
        
    }
}
