using UnityEngine;
using System.Collections;

public class Equipment : Singleton<Equipment>
{
	private IEquippable mEquippedTool;
	private IEquippable mEquippedAccessory;
	private Inventory.InventoryItem mItemShortcut;

	IEquippable EquippedTool
	{
		get
		{
			return this.mEquippedTool;
		}
		set
		{
			mEquippedTool = value;
		}
	}

	IEquippable EquippedAccessory
	{
		get
		{
			return this.mEquippedAccessory;
		}
		set
		{
			mEquippedAccessory = value;
		}
	}

	Inventory.InventoryItem EquippedItem
	{
		get
		{
			return this.mItemShortcut;
		}
		set
		{
			mItemShortcut = value;
		}
	}
	
}
