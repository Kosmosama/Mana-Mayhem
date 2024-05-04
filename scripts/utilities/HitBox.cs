using Godot;
using System;

// Manages getting hit
// The idea is that it tells its "parent" to -hp
public partial class HitBox : Area2D
{
	CollisionShape2D collision;
	Timer cooldownGetHit;
	
	public override void _Ready()
	{
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		cooldownGetHit = GetNode<Timer>("%CooldownGetHit");
	}

	private void OnAreaEntered(Area2D area)
	{
		
	}

	
}
