using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmDialog : MonoBehaviour {

    [SerializeField] private Text m_title;
    [SerializeField] private Text m_content;
    [SerializeField] private Text m_positiveText;
    [SerializeField] private Text m_negativeText;
    [SerializeField] private IDialog m_IDialogInteraction;
    
    string Title {
        get {
            return m_title.text;
        }
        set {
            m_title.text = value;
        }
    }

    string Content {
        get {
            return m_content.text;
        }
        set {
            m_content.text = value;
        }
    }

    string PositiveText {
        get {
            return m_positiveText.text;
        }
        set {
            m_positiveText.text = value;
        }
    }

    string NegativeText {
        get {
            return m_negativeText.text;
        }
        set {
            m_negativeText.text = value;
        }
    }
    
    public IDialog DialogInteractionListener {
        get {
            return m_IDialogInteraction;
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
        string title = "";
        string content = "";
        string positiveText = "OK";
        string negativeText = "Cancel";
        IDialog dialogInteractionListener = null;

        public Builder()
        {
            
        }
        
        public Builder setTitle(string title) {
            this.title = title;
            return this;
        }
        public Builder setContent(string content) {
            this.content = content;
            return this;
        }
        public Builder setPositiveText(string positiveText) {
            this.positiveText = positiveText;
            return this;
        }
        public Builder setNegativeText(string negativeText) {
            this.negativeText = negativeText;
            return this;
        }
        public Builder setDialogInteractionListener(IDialog interactionListener) {
            this.dialogInteractionListener = interactionListener;
            return this;
        }
        public ConfirmDialog Build() {
            var gameObject = Instantiate(Resources.Load("Prefab/ConfirmDialog")) as GameObject;
            ConfirmDialog confirmDialog = gameObject.GetComponent<ConfirmDialog>();
            confirmDialog.transform.SetParent(GameObject.Find("Modal Panel").transform);
            confirmDialog.transform.localPosition = new Vector3(0,0,0);
            confirmDialog.transform.localScale = new Vector3(1,1,1);
            
            confirmDialog.Title = title;
            confirmDialog.Content = content;
            confirmDialog.PositiveText = positiveText;
            confirmDialog.NegativeText = negativeText;
            confirmDialog.DialogInteractionListener = dialogInteractionListener;
            
            return confirmDialog;
        }
                
    }
}
