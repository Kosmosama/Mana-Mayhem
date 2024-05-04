using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	float movementSpeed = 300;
	[Export]
	float hp = 100;
	public float HP
	{
		get { return HP; }
		set
		{
			HP = (value < 0)? 0:value;
		}
	}

	[Export]
	float damage = 100;

	Sprite2D sprite;
	Timer timerInvincibilityFrames;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("GFX");
		timerInvincibilityFrames = GetNode<Timer>("TimerInvincibilityFrames");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
	}

	private void Movement()
	{
		float xMov = Input.GetActionStrength("right") - Input.GetActionStrength("left"); // -left +right
		float yMov = Input.GetActionStrength("down") - Input.GetActionStrength("up"); // -up +down
		Vector2 mov = new Vector2(xMov, yMov);

		SpriteFlipper(mov.X);
		Velocity = mov.Normalized() * movementSpeed;
		
		MoveAndSlide();
	}

	private void SpriteFlipper(double x)
	{
		if (x > 0)
		{
			sprite.FlipH = false;
		}
		else if (x < 0)
		{
			sprite.FlipH = true;
		}
	}

	public void Damage(float damageAmount)
	{
		hp -= damageAmount;
		GD.Print(hp);

		// Check if player is alive after damage
		if (this.IsAlive())
			// If it is alive, give him some invincibility to prevent instant death
			timerInvincibilityFrames.Start();
	}

	public bool CanBeAttacked()
	{
		return this.IsAlive() && timerInvincibilityFrames.IsStopped();
	}

	private bool IsAlive()
	{
		if (hp > 0)
		{
			return true;
		}
		else
		{
			OnPlayerDeath();
			return false;
		}
	}

	private void OnPlayerDeath()
	{
		// Player death logic
		GetTree().Quit();
	}
}
