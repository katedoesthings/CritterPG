using Godot;
using System;

public partial class CookingRecipe : Node
{
	[Export]
	public string [] stepsAndOrder;
    [Export]
    public float numChop;
    [Export]
    public float chopTime;
    [Export]
    public float numSpice;
    [Export]
    public float spiceMultiplier;
	[Export]
	public float spiceThreshold;
	[Export]
	public float bakeTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
