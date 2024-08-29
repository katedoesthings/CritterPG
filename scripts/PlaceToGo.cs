using Godot;
using System;

public partial class PlaceToGo : Node3D
{
	[Export]
	public float timeToGoHere;
	[Export]
	public float speedToGoHere;
	public Vector3 positionToGo;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		positionToGo = this.GlobalPosition;
		GD.Print(positionToGo);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
