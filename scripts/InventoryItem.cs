using Godot;
using System;

public partial class InventoryItem : Node3D
{
	[Export]
	public string name;
	[Export]
	public string desc;
	[Export]
	public Texture2D icon;
	[Export]
	public bool disappearOnPickedUp;
	[Export]
	public bool replenishable;
	[Export]
	public Label3D getLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void _on_area_3d_body_entered(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            getLabel.Visible = true;
        }
    }

    private void _on_area_3d_body_exited(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            getLabel.Visible = false;

        }
    }
}
