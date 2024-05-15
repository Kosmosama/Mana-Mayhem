using Godot;
using System;

public partial class XpOrb : Area2D
{
	[Export]
	float xpAmount = 1;

	public float XpAmount
	{
		get { return xpAmount; }
	}
	// on get, QueueFree?

	Sprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("GFX");

		BecomeRandomOrb();
	}

	public void BecomeRandomOrb()
	{
		Random random = new Random();
		int randNum = random.Next(1, 101);

		if (randNum <= 80)
		{
			// 80% chance of being small
			BeSmall();
		}
		else if (randNum <= 95)
		{
			// 15% chance of being medium
			BeMedium();
		}
		else
		{
			// 5% chance of being large
			BeLarge();
		}
	}

	private void BeSmall()
	{
		// Load the small orb sprite
		Texture2D texture = (Texture2D)GD.Load("res://sprites/world/Xp/XpOrbSmall.png");
		sprite.Texture = texture;
		xpAmount = 1;
	}

	private void BeMedium()
	{
		// Load the medium orb sprite
		Texture2D texture = (Texture2D)GD.Load("res://sprites/world/Xp/XpOrbMedium.png");
		sprite.Texture = texture;
		xpAmount = 1;
	}

	private void BeLarge()
	{
		// Load the large orb sprite
		Texture2D texture = (Texture2D)GD.Load("res://sprites/world/Xp/XpOrbLarge.png");
		sprite.Texture = texture;
		xpAmount = 1;
	}
}
