using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class BaseItem : CacheableObject
{
	[SerializeField] private int m_id;
    [SerializeField] private string m_name;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_sprite;
    
    public int Id {
		get {
			return this.m_id;
		}
		set {
			m_id = value;
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
