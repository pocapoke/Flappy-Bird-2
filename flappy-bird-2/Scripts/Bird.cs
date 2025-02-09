using Godot;
using System;

public partial class Bird : RigidBody2D
{
	[Export] float speed = 200f;
	[Export] float jumpPower = -400f;
	[Export] float resetTimer = 1f;
	[Export] AudioStreamMP3 flapSFX;
	[Export] AudioStreamMP3 scoreSFX;
	[Export] AudioStreamMP3 dieSFX;
	[Export] AudioStreamMP3 wallHitSFX;

	public bool crashed { get; private set; } = false;
	private Vector2 currentMove = new Vector2();
	private Vector2 flyUp = new Vector2();
	private float timeSinceDeath = 0f;
	private AudioStreamPlayer flapPlayer;
	private AudioStreamPlayer otherPlayer;
	private AudioStreamPlayer wallHitPlayer;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentMove.X = speed;
		flyUp.Y = jumpPower;

		flapPlayer = GetNode<AudioStreamPlayer>("Jump sound");
		otherPlayer = GetNode<AudioStreamPlayer>("Other sounds");
		wallHitPlayer = GetNode<AudioStreamPlayer>("Wall hit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!crashed) return;

		timeSinceDeath += (float)delta;

		if (timeSinceDeath > resetTimer)
		{
			 GameManager.Instance.Reload();
		}
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
		flapPlayer.Stream = flapSFX;
		flapPlayer.Play();
    }

	public void StopBird()
	{
		if (crashed) return;

		crashed = true;
		LinearVelocity = Vector2.Zero;
		GravityScale = 0f;
		LockRotation = false;

		DieSFX();

	}

	public void OnBodyEnter(Node body)
	{
		if (crashed) return;
		HitWallSFX();
		DieSFX();

		crashed = true;
	}

	public void PointSFX()
	{
		otherPlayer.Stream = scoreSFX;
		otherPlayer.Play();
	}

	public void DieSFX()
	{
		otherPlayer.Stream = dieSFX;
		otherPlayer.Play();
	}

	public void HitWallSFX()
	{
		wallHitPlayer.Stream = wallHitSFX;
		wallHitPlayer.Play();
	}
}
