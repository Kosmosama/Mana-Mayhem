using Godot;
using System;

public partial class XpOrb : Area2D
{
	[Export]
	float xpAmount;

	public float XpAmount
	{
		get { return xpAmount; }
	}

	Sprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("GFX");

		BecomeRandomOrb();
	}

	public void BecomeRandomOrb()
	{
		Random random = new Random();
		int randNum = random.Next(1, 71);

		if (randNum <= 35)
		{
			// 50% chance of being small
			BeSmall();
		}
		else if (randNum <= 60)
		{
			// 35.7% chance of being medium
			BeMedium();
		}
		else
		{
			// 14.2% chance of being large
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
