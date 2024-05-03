using Godot;
using System;

public partial class Player : CharacterBody2D
{
	Sprite2D sprite;
	
	// hp
	// speed
	// damage
	// armor
	// abilityCooldown
	// abilitySize

	// directionalMovement
	// abilities (spawns with 1 level of normalAttack)
	// uponDeathSaveTimeAndStateToLeaderboard

	
	[Export]
	float movementSpeed = 300;
	[Export]
	float hp = 100;
	[Export]
	float damage = 100;

	// [Export]
	// float armor = 100;
	// [Export]
	// float abilityCooldown = 100;
	// [Export]
	// float abilitySize = 100;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("GFX");
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
}
