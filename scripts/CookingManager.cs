using Godot;
using System;
using System.Drawing;

public partial class CookingManager : Control
{
	[Export]
	public Timer cookingTimer;
	[Export]
	public Control chopUI;
	[Export]
	public Slider chopSlider;
	[Export]
	public Control spiceUI;
	[Export]
	public Slider spiceSlider;
	[Export]
	public Control bakeUI;
	[Export]
	public ColorRect[] bakeTimer;
	[Export]
	public RichTextLabel chopCountText;
	[Export]
	public RichTextLabel chopPerformanceLabel;

	public float curTime;

	public int stepIndex = 0;

	public int curChop;

	public CookingRecipe curRecipe;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _TimeToCook(CookingRecipe theRecipe)
	{
		curRecipe = theRecipe;
		this.Visible = true;
		if (curRecipe.stepsAndOrder[stepIndex] == "chop")
		{
			_ChopStart();
		}
		if (curRecipe.stepsAndOrder[stepIndex] == "spice")
		{
			_ChopStart();
		}
		if (curRecipe.stepsAndOrder[stepIndex] == "bake")
		{
			_ChopStart();
		}
	}

	public void _ChopStart()
	{
		chopUI.Visible = true;
		curChop = 0;
		chopCountText.Text = curChop.ToString();
		//cookingTimer.Start();
	}

	public void _SpiceGrindStart()
	{
		spiceUI.Visible = true;
	}

	public void _BakeStart()
	{
		bakeUI.Visible = true;
	}

	private void _on_cooking_timer_timeout()
	{
		curTime += (float)cookingTimer.WaitTime;
		chopSlider.Value += (chopSlider.MaxValue / curRecipe.chopTime * cookingTimer.WaitTime);
		if (curTime >= curRecipe.chopTime)
		{
			curTime = 0;
			_ChopReset();
		}
	}

	public void _ChopReset()
	{
		chopSlider.Value = 0;
		curTime = 0;

	}

	public void _NextStep()
	{
		stepIndex++;
		GD.Print("Whoa buddy boy!");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (curRecipe != null)
		{
			if (curRecipe.stepsAndOrder[stepIndex] == "chop")
			{
				if (Input.IsActionJustReleased("leftclick"))
				{
					curChop++;
					chopCountText.Text = curChop.ToString();
					if (chopSlider.Value > 49 && chopSlider.Value < 51)
					{
						chopPerformanceLabel.Text = "Fuckin NUT!!!!";
					}
					else if (chopSlider.Value > 47 && chopSlider.Value < 53)
					{
						chopPerformanceLabel.Text = "Great!!";
					}
					else if (chopSlider.Value > 40 && chopSlider.Value < 60)
					{
						chopPerformanceLabel.Text = "Good!";
					}
					else if (chopSlider.Value > 30 && chopSlider.Value < 70)
					{
						chopPerformanceLabel.Text = "Ok!";
					}
					else
					{
						chopPerformanceLabel.Text = "Ass!";
					}
					_ChopReset();
					if (curChop > curRecipe.numChop)
					{
						if (stepIndex < curRecipe.stepsAndOrder.Length)
						{
							_NextStep();
						}
					}
				}
				curTime += (float)delta;
				chopSlider.Value += (chopSlider.MaxValue / curRecipe.chopTime * delta);
				if (curTime >= curRecipe.chopTime)
				{
					curTime = 0;
					_ChopReset();
				}
			}
        }
	}
}
