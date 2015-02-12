using UnityEngine;
using System.Collections;

public class Item
{
    [SerializeField] private string m_id;
    [SerializeField] private int m_tier;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private int m_price;
    [SerializeField] private string m_spriteName;
    
    public Item() {
    
    }
    
    public Item(string id, int tier, string name, string description, int price, string spriteName)
    {
        this.Id = id;
        this.Tier = tier;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.SpriteName = spriteName;
    }
    
    
    public Item(Item another)
    {
        this.Id = another.Id;
        this.Tier = another.Tier;
        this.Name = another.Name;
        this.Description = another.Description;
        this.Price = another.Price;
        this.SpriteName = another.SpriteName;
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
    
    public string Name
    {
        get
        {
            return this.m_name;
        }
        set
        {
            m_name = value;
        }
    }
    
    public string Description
    {
        get
        {
            return this.m_description;
        }
        set
        {
            m_description = value;
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
    
    public string SpriteName
    {
        get
        {
            return this.m_spriteName;
        }
        set
        {
            m_spriteName = value;
        }
    }        
    
    public Sprite Sprite {
        get {
            string path = "Sprite/" + SpriteName;
            return Resources.Load<Sprite>(path);
        }
        set {
            this.m_spriteName = value.name;
        }
    }
    
    public static Item FromId(string id) {
        if(id == "glove") {
            Item item = new Item(id, 0, "glove", "an angora gloves", 100, "icon_13");
            return item;
        }
        else if(id == "hat") {
            Item item = new Item(id, 0, "hat", "an angora hat", 100, "icon_12");
            return item;
        }else if(id == "hair_1") {
            Item item = new Item(id, 0, "normal hair", "an angora hat", 100, "hair_1");
            return item;
        }else if(id == "hair_2") {
            Item item = new Item(id, 0, "hat", "an angora hat", 100, "hair_2");
            return item;
        }
        return null;
    }
}
