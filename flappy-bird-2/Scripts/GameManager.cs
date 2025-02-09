using Godot;
using System;
using System.IO;
using System.Threading;

public partial class GameManager : Node
{
	public static GameManager Instance;

	[Export] PackedScene pipeScene;
	[Export] float gapBetweenPipes = 550f;
	[Export] Vector2 spawnHeightRange = new Vector2 (150f, 550f);
	[Export] Bird player;
	[Export] Node currentScene;
	[Export] RichTextLabel currentScoreLabel;
	[Export] RichTextLabel highScoreLabel;

	private float lastPlayersX = 0f;
	private float playersXTally = 0f;
	private int score = 0;
	private int highScore = 0;
	private string path;


	public override void _Ready()
	{
		Instance = this;

		string data;
		path = Path.Join(ProjectSettings.GlobalizePath("user://"), "HighScore.Txt");

		if (!File.Exists(path)) return;

		try
		{
			data = File.ReadAllText(path);

			int.TryParse(data, out highScore);
			highScoreLabel.Text = highScore.ToString();
		}
		catch(System.Exception)
		{
			//
		}
	}

	public override void _Process(double delta)
	{
		playersXTally += player.Position.X - lastPlayersX;

		lastPlayersX = player.Position.X;

		if (playersXTally >= gapBetweenPipes)
		{
			playersXTally = 0f;
			Node2D pipe = pipeScene.Instantiate<Node2D>();
			AddChild(pipe);

			float yPos = (float)GD.RandRange(spawnHeightRange.X, spawnHeightRange.Y);
			pipe.Position = new Vector2(player.Position.X + gapBetweenPipes, yPos);
		}	
	}

	public void Reload()
	{
		if (score > highScore)
		{
			File.WriteAllText(path, score.ToString());
		}

		currentScene.GetTree().ReloadCurrentScene();
	}

	public void Point()
	{
		score++;
		currentScoreLabel.Text = score.ToString();
		player.PointSFX();
	}

}
