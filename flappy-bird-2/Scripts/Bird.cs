using Godot;
using System;

public partial class Bird : RigidBody2D
{
	[Export] float speed = 200f;
	[Export] float jumpPower = -400f;
	[Export] float resetTimer = 1f;

	private Vector2 currentMove = new Vector2();
	private Vector2 flyUp = new Vector2();
	private bool crashed = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentMove.X = speed;
		flyUp.Y = jumpPower;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		

	}

    public override void _PhysicsProcess(double delta)
    {
        if (crashed) return;
		
		currentMove.Y = LinearVelocity.Y;

		LinearVelocity = currentMove;
    }

    public override void _Input(InputEvent @event)
	{
		if (crashed) return;
		
		if (@event is InputEventKey space && space.Pressed)
		{
			Fly();
		}
	}

	private void Fly()
	{
        currentMove.Y = 0f;
        LinearVelocity = currentMove;

        ApplyImpulse(flyUp);

		GetNode<AnimationPlayer>("FLY").Play("Fly");
    }

	public void StopBird()
	{
		crashed = true;
		LinearVelocity = Vector2.Zero;
		GravityScale = 0f;
		LockRotation = false;
	}
}
