using Godot;
using System;

public partial class ScheduledThing : Node
{
	[Export]
	PlaceToGo[] placesToGo;
	Schedule theSchedule;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		theSchedule = GetNode<Schedule>("/root/Schedule");
		theSchedule.TimePass += TimeHasPassed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void TimeHasPassed()
	{
		GD.Print("HOLY SIHTTTTT");
	}
}
