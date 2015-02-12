using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class BaseFood : CacheableObject
{
	[SerializeField] private int m_id;
	[SerializeField] private string m_name;
	[SerializeField] private string m_description;
	[SerializeField] private Sprite m_sprite;
	[SerializeField] private AudioClip m_biteSound;

	[SerializeField] private float m_growth;
	[SerializeField] private float m_generationProbability;
	[SerializeField] private float m_disappearTime;

	public int Id
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

	
	public AudioClip BiteSound
	{
		get
		{
			return this.m_biteSound;
		}
		set
		{
			m_biteSound = value;
		}
	}        

	public float Growth
	{
		get
		{
			return this.m_growth;
		}
		set
		{
			m_growth = value;
		}
	}
	
	public float GenerationProbability
	{
		get
		{
			return this.m_generationProbability;
		}
		set
		{
			m_generationProbability = value;
		}
	}

	public float DisappearTime
	{
		get
		{
			return this.m_disappearTime;
		}
		set
		{
			m_disappearTime = value;
		}
	}
}
