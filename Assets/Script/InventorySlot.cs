using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : CacheableObject {
    
    [SerializeField] private Item m_item;
    [SerializeField] private string m_itemId;
    [SerializeField] private int m_quantity;
    [SerializeField] private bool isEnabled = true;
    
    private Image imageview;
    private Text textview;
    
    Image Imageview {
        get {
            if(imageview == null) {
                imageview = gameObject.GetComponentsInChildren<Image>(true)[1];
            }
            return imageview;
        }
    }

    Text Textview {
        get {
            if(textview == null) {
                textview = gameObject.GetComponentsInChildren<Text>(true)[0];
            }
            return textview;
        }
    }
    
    public InventorySlot(Item item, int quantity = 1)
    {
        this.m_item = item;
        this.m_quantity = quantity;
    }
    
    public Item Item {
        get {
            return this.m_item;
        }
        set {
            m_item = new Item(value);
            Debug.Log("InvenotorySlot: " + this.ToString() + " /" + m_item);
            if(m_item != null) {
                Imageview.overrideSprite = m_item.Sprite;
                Imageview.enabled = true;
            } else {
                Imageview.overrideSprite = null;
                Imageview.enabled = false;
                Textview.text = "";                    
            }
        }
    }

    public string ItemId {
        get {
            return this.m_itemId;
        }
        set {
            m_itemId = value;
        }
    }

    public int Quantity {
        get {
            return this.m_quantity;
        }
        set {
            m_quantity = value;
            if(m_quantity > 0) {
                Textview.text = "x" + m_quantity;
            } else {
                Item = null;
            }                    
            
        }
    }
    
    public bool Enabled {
        get {
            return this.isEnabled;
        }
        set {
            isEnabled = value;
        }
    }
    
    
}
