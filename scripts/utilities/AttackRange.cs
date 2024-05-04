using Godot;
using System;

// Manages hitting
public partial class AttackRange : Area2D
{
	[Export]
	float damage = 1;

	public override void _Ready()
	{
		
	}

	// [Export] groupToAttack;
	// BodyEntered -> save in a list so it can/cannot be damaged again
}
