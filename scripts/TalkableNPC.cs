using Godot;
using System;
using DialogueManagerRuntime;
using System.IO;

public partial class TalkableNPC : Node
{
	[Export]
	Resource dialogue;
	[Export]
	String dialogueStart;
    
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void speakTo()
	{
        DialogueManager.ShowExampleDialogueBalloon(dialogue, "start");
    }

    private void _OnAreaEntered(Node3D body)
	{
        GD.Print("Holy Shit!!! Entered");
        if (body.IsInGroup("Player"))
		{
			GD.Print("Holy Shit!!! ThE Player");
		}
	}
}
