using Godot;
using System;

public partial class NormalEnemy : CharacterBody2D, IEnemy
{
	[Export] 
	float movementSpeed = 200;
	[Export] 
	float hp = 10;

	[Export]
	float damage = 10;
	bool isPlayerWithinRange;

	Player player;
	Sprite2D sprite;
	Timer timerAttackCooldown;
	Timer timerInvincibilityFrames;

	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Player;
		sprite = GetNode<Sprite2D>("GFX");
		timerAttackCooldown = GetNode<Timer>("TimerAttackCooldown");
		timerInvincibilityFrames = GetNode<Timer>("TimerInvincibilityFrames");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
		Attack();
	}

	public void Movement()
	{
		Vector2 direction = Position.DirectionTo(player.Position);
		Velocity = direction * movementSpeed;
		SpriteFlipper(direction.X);
		
		MoveAndSlide();
	}
	
	private void SpriteFlipper(double x)
	{
		if (x > 0)
		{
			sprite.FlipH = true;
		}
		else if (x < 0)
		{
			sprite.FlipH = false;
		}
	}

	public void Attack()
	{
		if (timerAttackCooldown.IsStopped() && isPlayerWithinRange && player.CanBeAttacked())
		{
			player.Damage(damage);
			timerAttackCooldown.Start();
		}
	}

	public Vector2 GetPosition()
	{
		return Position;
	}

	private bool IsAlive()
	{
		if (hp > 0)
		{
			return true;
		}
		else
		{
			OnDeath();
			return false;
		}
	}

	public void Damage(float damageAmount)
	{
		hp -= damageAmount;

		if (IsAlive())
			MakeInvincible();
	}

	public void MakeInvincible()
	{
		timerInvincibilityFrames.Start();
		sprite.SelfModulate = new Color(1, 1, 1, 0.5f);
	}

	public bool CanBeAttacked()
	{
		return this.IsAlive() && timerInvincibilityFrames.IsStopped();
	}

	public void OnDeath()
	{
		// Enemy death logic (spawn xp)
		QueueFree();
	}

	private void OnInvincibilityTimeout()
	{
		sprite.SelfModulate = new Color(1,1,1,1);
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			GD.Print("Player within range");
			isPlayerWithinRange = true;
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player)
		{
			GD.Print("Player exited range");
			isPlayerWithinRange = false;
		}
	}
}
