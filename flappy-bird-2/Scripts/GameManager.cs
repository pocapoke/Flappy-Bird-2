using Godot;
using System;
using System.IO;

public partial class GameManager : Node
{
	public static GameManager Instance;

	[Export] PackedScene pipe;
	[Export] float gapBetweenPipes = 550f;
	[Export] Vector2 spawnHeightRange = new Vector2 (150f, 550f);
	[Export] Bird player;

	public override void _Ready()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{

	}

}
