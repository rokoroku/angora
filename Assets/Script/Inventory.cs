using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{
	private readonly Dictionary<int, InventoryItem> m_itemTable = new Dictionary<int, InventoryItem>();

	public void AddItem(BaseItem item, int quantity = 1)
	{
		if (m_itemTable.ContainsKey(item.Id))
		{
			InventoryItem inventoryItem = GetInventoryItem(item.Id);
			inventoryItem.quantity += quantity;
		} else
		{
			m_itemTable.Add(item.Id, new InventoryItem(item, quantity));
		}
	}
	
	public void RemoveItem(BaseItem item, int quantity = 1)
	{
		if (m_itemTable.ContainsKey(item.Id))
		{
			InventoryItem inventoryItem = GetInventoryItem(item.Id);
			inventoryItem.quantity -= quantity;
			if (inventoryItem.quantity <= 0)
			{
				m_itemTable.Remove(item.Id);
			}
		} else
		{
			m_itemTable.Add(item.Id, new InventoryItem(item, quantity));
		}
	}
		
	public InventoryItem GetInventoryItem(int id)
	{
		InventoryItem result;
		if (m_itemTable.TryGetValue(id, out result))
		{
			return result;
		}
		return null;
	}

	public ICollection<InventoryItem> GetInventoryItems()
	{
		return m_itemTable.Values;
	}

	public class InventoryItem
	{
		public BaseItem item;
		public int quantity;
		
		public InventoryItem(BaseItem item, int quantity = 1)
		{
			this.item = item;
			this.quantity = quantity;
		}
	};

}
