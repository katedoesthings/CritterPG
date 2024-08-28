using Godot;
using System;

public partial class Schedule : Node
{

	[Export]
	float howLongHour;

	public static float currentTime;

	public static Timer ScheduleTimer;

	[Signal]
	public delegate void TimePassEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentTime = 0;
		ScheduleTimer = GetNode<Timer>("%ScheduleTimer");
		if (ScheduleTimer != null)
		{
			ScheduleTimer.WaitTime = howLongHour;
			ScheduleTimer.Start();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_timer_timeout()
	{
		ScheduleTimer.WaitTime = howLongHour;
        ScheduleTimer.Start();
		EmitSignal(nameof(this.TimePass));
		if (currentTime < 24)
		{
			currentTime += 1;
		}
		else
		{
			currentTime = 0;
		}
		GD.Print("The time is now: " + currentTime.ToString());
    }

}