using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class ItemManager : Node
{
	// Called when the node enters the scene tree for the first time.

	public static Dictionary allRecipes;

	public override void _Ready()
	{
		allRecipes = new Dictionary();

		string [] dir = DirAccess.GetFilesAt("res://Recipes");
		if (dir != null )
		{
			for (int i = 0; i<dir.Length; i++)
			{
                GD.Print("res://Recipes/" + dir[i]);
                allRecipes[dir[i]] = ResourceLoader.Load(("res://Recipes/" + dir[i]));
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
