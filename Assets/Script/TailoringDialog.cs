using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TailoringDialog : MonoBehaviour {

    [SerializeField] private Image m_itemImage;
    [SerializeField] private Text m_itemTitle;
    
    private Sketch m_sketch;
    private IDialog m_IDialogInteraction;
    private List<RequirementSlot> m_requirementSlotList;
    
    public Sketch Sketch {
        get {
            return this.m_sketch;
        }
        set {
            m_sketch = value;
            if(m_sketch != null) {
                var item = Item.FromId(m_sketch.ItemId) as Item;
                ImageUtil.ChangeSprite(m_itemImage.gameObject, item.Sprite);
                m_itemImage.enabled = true;
                
                m_itemTitle.text = item.Name;
                m_itemTitle.enabled = true;
                
                for(int i=0; i<m_sketch.Requirements.Count; i++) {
                    Sketch.Requirement requirement = m_sketch.Requirements[i];
                    RequirementSlotList[i].Requirement = requirement;
                }
            }
        }
    }
    
    List<RequirementSlot> RequirementSlotList {
        get {
            if(m_requirementSlotList == null) m_requirementSlotList = new List<RequirementSlot>(gameObject.GetComponentsInChildren<RequirementSlot>(true));
            return this.m_requirementSlotList;
        }
    }
    
    IDialog DialogInteractionListener {
        get {
            return this.m_IDialogInteraction;
        }
        set {
            m_IDialogInteraction = value;
        }
    }
    
    public void PositiveClick() {
        if(DialogInteractionListener != null) DialogInteractionListener.OnPositiveClick();
        Destroy(gameObject);
    }
    
    public void NegativeClick() {
        if(DialogInteractionListener != null) DialogInteractionListener.OnNegativeClick();
        Destroy(gameObject);
    }
    
    public void Cancel() {
        if(DialogInteractionListener != null) DialogInteractionListener.OnCancel();
        Destroy(gameObject);
    }
    
    public class Builder {
        Sketch sketch = null;
        IDialog dialogInteractionListener = null;
        
        public Builder()
        {
            
        }
        
        public Builder SetSketch(Sketch sketch) {
            this.sketch = new Sketch(sketch);
            return this;
        }
        
        public Builder SetDialogInteractionListener(IDialog interactionListener) {
            this.dialogInteractionListener = interactionListener;
            return this;
        }
        
        public TailoringDialog Build() {
            var gameObject = Instantiate(Resources.Load("Prefab/TailorConfirmDialog")) as GameObject;
            TailoringDialog confirmDialog = gameObject.GetComponent<TailoringDialog>();
            confirmDialog.transform.SetParent(GameObject.Find("Modal Panel").transform);
            confirmDialog.transform.localPosition = new Vector3(0,0,0);
            confirmDialog.transform.localScale = new Vector3(1,1,1);
            
            confirmDialog.Sketch = sketch;
            confirmDialog.DialogInteractionListener = dialogInteractionListener;
            
            return confirmDialog;
        }
        
    }
}
