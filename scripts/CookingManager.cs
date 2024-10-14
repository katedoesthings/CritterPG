using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

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
    public RichTextLabel spiceCountText;
    [Export]
	public RichTextLabel chopPerformanceLabel;
	[Export]
	public RichTextLabel spicePerformanceLabel;
    [Export]
    public Slider spiceThresholdSlider;
    [Export]
    public RichTextLabel bakePerformanceLabel;

    [Export]
	public ItemList recipeList;

    public List<CookingRecipe> recipeInventory = new List<CookingRecipe>();

    public float curTime;

	public int stepIndex = 0;

	public int curChop;

	public int curSpice;

	public float curBake = 0;

	public int curBakeStep = 0;

	[Export]
	public float transitionTime;

	private float curTransitionTime;

	[Export]
	public RichTextLabel resultsLabel;

    [Export]
    public Control resultsParent;

    [Export]
    InventoryManager inventoryManager;

	[Export]
	character theCharacter;

    public CookingRecipe curRecipe;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		curTransitionTime = transitionTime;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _FullReset()
	{
		curChop = 0;
		curSpice = 0;
		curBake = 0;
		curBakeStep = 0;
		curTime = 0;
		stepIndex = 0;
		bakePerformanceLabel.Text = "";
		spicePerformanceLabel.Text = "";
		chopPerformanceLabel.Text = "";
	}

	public void _PickRecipe()
	{
        _ClearUI();
        recipeList.Visible = true;
        this.Visible = true;
    }

	private void _on_recipe_list_item_clicked(int index, Vector2 at_position, int mouse_button_index)
	{
        CookingRecipe testRecipe = (CookingRecipe)((PackedScene)ItemManager.allRecipes[recipeList.GetItemText(index)]).Instantiate();
        recipeList.Visible = false;
        _TimeToCook(testRecipe);
    }


    public void _TimeToCook(CookingRecipe theRecipe)
	{
		_FullReset();
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
        spiceUI.Visible = false;
        bakeUI.Visible = false;
        curChop = 0;
		chopCountText.Text = curChop.ToString();
		//cookingTimer.Start();
	}

	public void _SpiceGrindStart()
	{
		spiceUI.Visible = true;
        chopUI.Visible = false;
        bakeUI.Visible = false;
		spiceThresholdSlider.Value = curRecipe.spiceThreshold;
		curSpice = 0;
    }

	public void _BakeStart()
	{
		bakeUI.Visible = true;
		spiceUI.Visible = false;
        chopUI.Visible = false;
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

	public void _SpiceReset()
	{
        spiceSlider.Value = 0;
    }

	
	public void _Transition()
	{
        curTransitionTime = 0;
    }
	public void _NextBakeStep()
	{
		bakeTimer[curBakeStep].Color = new Godot.Color(0, 1, 0, 1);
		curBakeStep++;
		curBake = 0;
	}

	public void _Results()
	{
        _ClearUI();
		this.Visible = true;
        resultsParent.Visible = true;
		resultsLabel.Text = "You cooked a " + curRecipe.recipeName +"!";
        InventoryItem resultItem = (InventoryItem)((PackedScene)curRecipe.resultItem).Instantiate();
		inventoryManager.AddToInventory(resultItem);
		resultItem.QueueFree();
	}

	private void _on_finished_cooking_button_button_down()
	{
        theCharacter.FreezeAndMouseUsable();
		_ClearUI();
    }

    public void _ClearUI()
	{
        spiceUI.Visible = false;
        chopUI.Visible = false;
        bakeUI.Visible = false;
		recipeList.Visible = false;
		resultsParent.Visible = false;
        this.Visible = false;
    }

	public void _NextStep()
	{
		stepIndex++;
		if (stepIndex < curRecipe.stepsAndOrder.Length)
		{
			if (curRecipe.stepsAndOrder[stepIndex] == "chop")
			{
				_ChopStart();
			}
			else if (curRecipe.stepsAndOrder[stepIndex] == "spice")
			{
				_SpiceGrindStart();
			}
			else if (curRecipe.stepsAndOrder[stepIndex] == "bake")
			{
				_BakeStart();
			}
		}
		else
		{
			_Results();
		}
    }

	public override void _PhysicsProcess(double delta)
	{
		if (curTransitionTime >= transitionTime)
		{
			if (curRecipe != null && stepIndex < curRecipe.stepsAndOrder.Length)
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
						if (curChop >= curRecipe.numChop)
						{
                            _Transition();
                            return;
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
				if (curRecipe.stepsAndOrder[stepIndex] == "spice")
				{
					if (Input.IsActionPressed("leftclick"))
					{
						spiceSlider.Value += curRecipe.spiceMultiplier;
					}
					if (Input.IsActionJustReleased("leftclick"))
					{
						curSpice++;
						spiceCountText.Text = curSpice.ToString();
						if (spiceSlider.Value > curRecipe.spiceThreshold - 1 && spiceSlider.Value < curRecipe.spiceThreshold + 1)
						{
							spicePerformanceLabel.Text = "Fuckin NUT!!!!";
						}
						else if (spiceSlider.Value > curRecipe.spiceThreshold - 3 && spiceSlider.Value < curRecipe.spiceThreshold + 3)
						{
							spicePerformanceLabel.Text = "Great!!";
						}
						else if (spiceSlider.Value > curRecipe.spiceThreshold - 10 && spiceSlider.Value < curRecipe.spiceThreshold + 10)
						{
							spicePerformanceLabel.Text = "Good!";
						}
						else if (spiceSlider.Value > curRecipe.spiceThreshold - 20 && chopSlider.Value < curRecipe.spiceThreshold + 20)
						{
							spicePerformanceLabel.Text = "Ok!";
						}
						else
						{
							spicePerformanceLabel.Text = "Ass!";
						}
						_SpiceReset();
						if (curSpice >= curRecipe.numSpice)
						{
                            _Transition();
                            return;
						}
					}
				}
				if (curRecipe.stepsAndOrder[stepIndex] == "bake")
				{
					curBake += (float)delta;
					if (curBake > curRecipe.bakeTime / bakeTimer.Length)
					{
						_NextBakeStep();
					}
					if (Input.IsActionJustReleased("leftclick"))
					{
						if (curBakeStep == bakeTimer.Length && curBake < (curRecipe.bakeTime / bakeTimer.Length) / 4)
						{
							bakePerformanceLabel.Text = "Fuckin NUT!!!!";
						}
						else if (curBakeStep == bakeTimer.Length && curBake < (curRecipe.bakeTime / bakeTimer.Length) / 2)
						{
							bakePerformanceLabel.Text = "Great!!";
						}
						else if (curBakeStep == bakeTimer.Length)
						{
							bakePerformanceLabel.Text = "Good!";
						}
						else if (curBakeStep == bakeTimer.Length - 1)
						{
							bakePerformanceLabel.Text = "Ok!";
						}
						else
						{
							bakePerformanceLabel.Text = "Ass!";
						}
						_Transition();
						return;
					}
				}
			}
		}
		else
		{
			curTransitionTime += (float)delta;
			if (curTransitionTime >= transitionTime)
			{
				_NextStep();
			}

        }
	}
}
