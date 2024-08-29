using Godot;
using System;
using DialogueManagerRuntime;
using System.IO;

public partial class TalkableNPC : Node3D
{
	[Export]
	Resource dialogue;
	[Export]
	String dialogueStart;
    [Export]
    Label3D InteractableLabel;

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

    private void _on_area_3d_body_entered(Node3D body)
	{
        if (body.IsInGroup("Player"))
		{
			InteractableLabel.Visible = true;
		}
	}

	private void _on_area_3d_body_exited(Node3D body)
	{
        if (body.IsInGroup("Player"))
        {
            InteractableLabel.Visible = false;

        }
    }

 }
