using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RequirementSlot : CacheableObject {
    
    private Sketch.Requirement m_requriement;
    private Image imageview;
    private Text textview;
        
    public Image Imageview {
        get {
            if(imageview == null) {
                imageview = gameObject.GetComponentsInChildren<Image>(true)[0];
            }
            return imageview;
        }
    }
    
    public Text Textview {
        get {
            if(textview == null) {
                textview = gameObject.GetComponentsInChildren<Text>(true)[0];
            }
            return textview;
        }
    }
    
    public Sketch.Requirement Requirement {
        get {
            return this.m_requriement;
        }
        set {
            if(value != null) {
                m_requriement = new Sketch.Requirement(value);
                gameObject.SetActive(true);
                Imageview.overrideSprite = Item.FromId(m_requriement.ItemId).Sprite;
                Imageview.enabled = true;
                Textview.text = "x" + m_requriement.Quantity; 
                Textview.enabled = true;
            } else {    
                m_requriement = null;
                Imageview.overrideSprite = null;
                Imageview.enabled = false;
                Textview.text = "";                    
                Textview.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
}    
    