using Godot;
using System;

public partial class MagicBolt : Area2D
{
	/*
	tween
	targetLocation
	penetration
	duration
	angle = GlobalPosition.DirectionTo(targetLocation);
	rotation = angle.Angle() + Mathf.DegToRad(135);
	*/
	public Vector2 targetLocation;

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{

	}
}
