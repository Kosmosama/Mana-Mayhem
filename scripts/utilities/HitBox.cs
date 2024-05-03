using Godot;
using System;

// Manages getting hit
public partial class HitBox : Area2D
{
	CollisionShape2D collision;
	Timer cooldownGetHit;

	[Signal]
	public delegate void HurtEventHandler(double damage);

	public override void _Ready()
	{
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		cooldownGetHit = GetNode<Timer>("%CooldownGetHit");
	}

	private void OnAreaEntered(Area2D area)
	{
		
	}
}
