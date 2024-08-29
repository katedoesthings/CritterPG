
using Godot;
using System;

public partial class ScheduledThing : CharacterBody3D
{
	[Export]
	PlaceToGo[] placesToGo;
	Schedule theSchedule;

	//this is the parent node
	[Export]
	Node3D thatsMyDad;

	public bool timeToGo;
	public Vector3 goingHere;
	public float goingSpeed;
	public float acceleration;

	[Export]
	NavigationAgent3D NPCNavAgent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		theSchedule = GetNode<Schedule>("/root/Schedule");
		theSchedule.TimePass += TimeHasPassed;
		NPCNavAgent = GetNode<NavigationAgent3D>("%NPCNavAgent");
		timeToGo = false;
		acceleration = 10;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (timeToGo)
		{

            Vector3 direction;

			NPCNavAgent.TargetPosition = goingHere;
            direction = NPCNavAgent.GetNextPathPosition() - GlobalPosition;
			direction = direction.Normalized() * goingSpeed;

			Velocity = Velocity.Lerp(direction, (float)(acceleration * delta));

            MoveAndSlide();

			if (GlobalPosition.X <= goingHere.X + .1f && GlobalPosition.X >= goingHere.X - .1f && GlobalPosition.Z <= goingHere.Z + .1f && GlobalPosition.Z >= goingHere.Z - .1f)
            {
				timeToGo = false;
			}
        }
	}

    private void TimeHasPassed()
	{
		for (int i = 0; i < placesToGo.Length; i++)
		{
			if (placesToGo[i].timeToGoHere == Schedule.currentTime)
			{
				timeToGo = true;
				goingHere = placesToGo[i].positionToGo;
				goingSpeed = placesToGo[i].speedToGoHere;
			}
		}
	}

	private void GoingPlaces(Vector3 thePlace)
	{
		Vector3 direction;

		NPCNavAgent.TargetPosition = thePlace;
		direction = NPCNavAgent.GetNextPathPosition();
	}
}
