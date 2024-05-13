using Godot;
using System;

public partial class MagicBolt : Area2D
{
	/*
	tween
	duration
	angle = GlobalPosition.DirectionTo(targetLocation);
	rotation = angle.Angle() + Mathf.DegToRad(135);
	*/
	Vector2 targetLocation;
	public Vector2 TargetLocation{set{targetLocation = value;}}
	Vector2 angle;

	float attackSize;
	float speed;
	int penetration;

	Tween tween;

	public override void _Ready()
	{
		angle = GlobalPosition.DirectionTo(targetLocation);
		Rotation = angle.Angle() + Mathf.DegToRad(135);

		speed = 200f;

		// tween = CreateTween();
		// tween.TweenProperty(this,"scale",new Vector2(1,1)*attackSize,1f).SetTrans(Tween.TransitionType.Quint).SetEase(Tween.EaseType.Out);
		// tween.Play();
	}

	public override void _Process(double delta)
	{
		Position += angle * speed * (float)delta;
	}

	// deal damage to enemy hit
	// queueFree on duration end (Timer)
}
