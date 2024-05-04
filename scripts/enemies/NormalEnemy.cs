using Godot;
using System;

public partial class NormalEnemy : CharacterBody2D
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

	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Player;
		sprite = GetNode<Sprite2D>("GFX");
		timerAttackCooldown = GetNode<Timer>("%TimerAttackCooldown");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
		Attack();
	}

	private void Movement()
	{
		Vector2 direction = Position.DirectionTo(player.Position);
		Velocity = direction * movementSpeed;
		SpriteFlipper(direction.X);
		
		MoveAndSlide();
	}
	
	private void Attack()
	{
		if (timerAttackCooldown.IsStopped() && isPlayerWithinRange && player.IsAlive())
		{
			player.HP -= damage;
			GD.Print(player.HP);
			timerAttackCooldown.Start();
		}
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
