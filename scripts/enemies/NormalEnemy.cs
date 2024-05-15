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
	Timer timerDamageFeedback;
	PackedScene xpOrb;

	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Player;
		sprite = GetNode<Sprite2D>("GFX");
		timerAttackCooldown = GetNode<Timer>("TimerAttackCooldown");
		timerDamageFeedback = GetNode<Timer>("TimerDamageFeedback");
		xpOrb = (PackedScene)ResourceLoader.Load("res://scenes/world/Xp/XpOrb.tscn");
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

	public bool IsAlive()
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
			DamageFeedback();
	}

	public void DamageFeedback()
	{
		timerDamageFeedback.Start();
		sprite.SelfModulate = new Color(1, 1, 1, 0.5f);
	}

	private void GenerateXpOrb()
	{
		Node xpOrbContainer = GetTree().GetNodesInGroup("XpOrbContainer")[0];

		XpOrb instantiableXpOrb = (XpOrb)xpOrb.Instantiate();
		instantiableXpOrb.Position = Position;

		CallDeferred("add_xp_orb", xpOrbContainer, instantiableXpOrb);
	}

	private void add_xp_orb(Node parent, Node child)
	{
		parent.AddChild(child);
	}

	public void OnDeath()
	{
		GenerateXpOrb();
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
