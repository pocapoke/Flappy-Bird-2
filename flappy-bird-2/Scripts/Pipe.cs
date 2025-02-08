using Godot;
using System;

public partial class Pipe : Node2D
{
	[Export] NinePatchRect upper;
	[Export] NinePatchRect lower;
	[Export] float groundHeight;

	bool hasSetSprite = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (hasSetSprite) return;

		if (lower != null)
		{
			hasSetSprite = true;
			Vector2 newSize = lower.Size;
			newSize.Y = groundHeight - lower.GlobalPosition.Y;
			lower.Size = newSize;
		}
	}
}
