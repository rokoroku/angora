using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TailoringSlot : MonoBehaviour {

    [SerializeField] private string m_sketchId;
    [SerializeField] private bool isEnabled = true;
    
    [SerializeField] private Image m_itemImage;
    [SerializeField] private Text m_titleText;
    [SerializeField] private Text m_remainingTimeText;
    
    [SerializeField] private float m_startTime;
    [SerializeField] private float m_finishTime;

    private Sketch m_sketch;
    private Item m_item;     
    
    public Sketch Sketch {
        get {
            return this.m_sketch;
        }
        set {
            m_sketch = value;
        }
    }
    
    public float StartTime {
        get {
            return this.m_startTime;
        }
        set {
            m_startTime = value;
        }
    }

    public float FinishTime {
        get {
            return this.m_finishTime;
        }
        set {
            m_finishTime = value;
        }
    }
    
    public float RemainingTime {
        get {
            return this.m_finishTime - Time.time;
        }
    }
    
	// Use this for initialization
	void Start () {
        SetItem(m_sketch);
	}
	
	// Update is called once per frame
	void Update () {
    	if(m_item != null) {
            m_remainingTimeText.text = "Remaining Time\n" + FormatSeconds(RemainingTime);
        }
	}
    
    public void SetItem(Sketch sketch) {
        if(sketch != null) {
            m_sketchId = sketch.Id;
            Sketch = new Sketch(sketch);
            Debug.Log("ItemId:" + sketch.ItemId);
            m_item = Item.FromId(sketch.ItemId) as Item;
        } else {
            m_item = null;
            Sketch = null;
        }
        Debug.Log("Item" + m_item + "/ItemId" + Sketch);
        if(m_item != null) {
            StartTime = Time.time;
            FinishTime = Time.time + sketch.RequiredTime;
            Debug.Log("Setted");
            
            m_itemImage.enabled = true;
            m_titleText.enabled = true;
            m_remainingTimeText.enabled = true;
            
            m_remainingTimeText.text = "Remaining Time\n" + FormatSeconds(RemainingTime);
            m_titleText.text = m_item.Name;
            ImageUtil.ChangeSprite(m_itemImage.gameObject, m_item.Sprite);
            
        } else {
            m_itemImage.enabled = false;
            m_titleText.enabled = false;
            m_remainingTimeText.enabled = false;
        }
    }
    
    public void Clear() {
        Sketch = null;
        StartTime = 0f;
        FinishTime = 0f;
        m_itemImage.enabled = false;
        m_titleText.enabled = false;
        m_remainingTimeText.enabled = false;
    }
    
    string FormatSeconds(float elapsed)
    {
        int d = (int)(elapsed * 100.0f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;
        int hundredths = d % 100;
        return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}
