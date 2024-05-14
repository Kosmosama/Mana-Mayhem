using Godot;
using System;

public partial class MagicBolt : Area2D
{
	/*
	tween
	duration
	*/
	Vector2 targetLocation;
	public Vector2 TargetLocation{set{targetLocation = value;}}
	Vector2 angle;

	float damageAmplification;
	float damage;
	float attackSize;
	float magicDuration;
	float speed;
	int penetration;

	Tween tween;
	Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("TimerDuration");

		angle = GlobalPosition.DirectionTo(targetLocation);
		Rotation = angle.Angle();

		// Multiplicators
		damageAmplification = 1;
		magicDuration = 1;
		attackSize = 1;

		// Stats
		damage = 2 * damageAmplification;
		speed = 400f;

		timer.WaitTime = 2 * magicDuration;
		timer.Start();

		// Animation
		tween = CreateTween();
		tween.TweenProperty(this,"scale",new Vector2(1,1)*attackSize,0.6f).SetTrans(Tween.TransitionType.Quint).SetEase(Tween.EaseType.Out);
		tween.Play();
	}

	public override void _Process(double delta)
	{
		Position += angle * speed * (float)delta;
	}

	private void OnTimerDurationTimeout()
	{
		QueueFree();
	}

	public void AttackEnemy(IEnemy enemy)
	{
		if (enemy.CanBeAttacked())
		{
			enemy.Damage(damage);
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is IEnemy enemy)
		{
			AttackEnemy(enemy);
		}
	}
}
