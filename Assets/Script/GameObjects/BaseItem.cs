using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class BaseItem : CacheableObject
{
    [SerializeField] private string m_id;
    [SerializeField] private int m_tier;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private int m_price;
    [SerializeField] private Sprite m_sprite;
    
    public Item Item
    {
        get
        {
            return new Item(m_id, m_tier, m_name, m_description, m_price, m_sprite.name);
        }
    }
    
    public string Id
    {
        get
        {
            return this.m_id;
        }
        set
        {
            m_id = value;
        }
    }

    public int Tier
    {
        get
        {
            return this.m_tier;
        }
        set
        {
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

    public int Price
    {
        get
        {
            return this.m_price;
        }
        set
        {
            m_price = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return this.m_sprite;
        }
        set
        {
            m_sprite = value;
        }
    }

}
