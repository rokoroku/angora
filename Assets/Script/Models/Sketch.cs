using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sketch {

    [SerializeField] private string m_id;
    [SerializeField] private string m_itemId;
    [SerializeField] private int m_tier;
    [SerializeField] private int m_price;
    [SerializeField] private float m_requiredTime;
    [SerializeField] private List<Requirement> m_requirements;
    
    public Sketch(string id, string itemId, int tier, int price, float requiredTime, List<Requirement> requirementList)
    {
        this.Id = id;
        this.Tier = tier;
        this.ItemId = itemId;
        this.Price = price;
        this.RequiredTime = requiredTime;
        this.Requirements = requirementList;
    }
    
    public Sketch(Sketch another)
    {
        this.Id = another.Id;
        this.Tier = another.Tier;
        this.ItemId = another.ItemId;
        this.Price = another.Price;
        this.RequiredTime = another.RequiredTime;
        this.Requirements = new List<Requirement>(another.Requirements);
    }
    
    
    public string Id {
        get {
            return this.m_id;
        }
        set {
            m_id = value;
        }
    }

    public int Tier {
        get {
            return this.m_tier;
        }
        set {
            m_tier = value;
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

    public List<Requirement> Requirements {
        get {
            return this.m_requirements;
        }
        set {
            m_requirements = value;
        }
    }

    public int Price {
        get {
            return this.m_price;
        }
        set {
            m_price = value;
        }
    }
    
    public float RequiredTime {
        get {
            return this.m_requiredTime;
        }
        set {
            m_requiredTime = value;
        }
    }
    
    public class Requirement {
    
        [SerializeField] private string m_itemId;
        [SerializeField] private int m_quantity;

        public Requirement()
        {

        }
        
        public Requirement(string itemId, int quantity)
        {
            this.ItemId = itemId;
            this.Quantity = quantity;
        }        
        
        public Requirement(Requirement another)
        {
            this.ItemId = another.ItemId;
            this.Quantity = another.Quantity;
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
            }
        }
        
    }    
}
