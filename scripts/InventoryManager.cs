using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryManager : Node
{
	[Export]
	public Panel inventoryPanel;

	[Export]
	public ItemList inventoryList;

    [Export]
    public RichTextLabel itemDescription;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ToggleInventory()
	{
		if (inventoryPanel.Visible)
		{
			inventoryPanel.Visible = false;
		}
		else
		{
			inventoryPanel.Visible = true;
		}
	}

	public void AddToInventory(InventoryItem theItem)
	{
		inventoryList.AddItem(theItem.name, theItem.icon);
		inventory.Add(theItem);
		if (theItem.disappearOnPickedUp)
		{
			if (theItem.replenishable)
			{
				theItem.Visible = false;
			}
			else
			{
				theItem.QueueFree();
			}
		}
	}

    private void _on_item_list_item_selected(int index)
    {
		itemDescription.Text = inventory[index].desc;
    }
}
