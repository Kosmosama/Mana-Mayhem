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

	Player player;
	Sprite2D sprite;

	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("player") as Player;
		sprite = GetNode<Sprite2D>("GFX");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
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
}
