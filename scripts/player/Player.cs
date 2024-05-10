using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	[Export]
	float movementSpeed = 300;
	[Export]
	float maxHp = 100;

	float hp;

	public float HP
	{
		get { return HP; }
		set
		{
			HP = (value < 0)? 0:value;
		}
	}

	[Export]
	float damage = 1;

	List<IEnemy> enemiesWithinRange;
	

	Sprite2D sprite;
	Timer timerInvincibilityFrames;
	ProgressBar progressBarHp;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("GFX");
		timerInvincibilityFrames = GetNode<Timer>("TimerInvincibilityFrames");
		progressBarHp = GetNode<ProgressBar>("ProgressBar");

		enemiesWithinRange = new List<IEnemy>();

		Initialize();
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
	}

	public void Initialize()
	{
		hp = maxHp;
		progressBarHp.MaxValue = maxHp;
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

	public void Damage(float damageAmount)
	{
		hp -= damageAmount;
		UpdateHpLabel();

		if (this.IsAlive())
			MakeInvincible();
	}

	public void MakeInvincible()
	{
		timerInvincibilityFrames.Start();
		sprite.SelfModulate = new Color(1, 1, 1, 0.5f);
		// color to make red-ish => sprite.SelfModulate = new Color(1, 0.54f, 0.52f, 0.8f);
	}

	public bool CanBeAttacked()
	{
		return this.IsAlive() && timerInvincibilityFrames.IsStopped();
	}

	private bool IsAlive()
	{
		if (hp > 0)
		{
			return true;
		}
		else
		{
			OnPlayerDeath();
			return false;
		}
	}

	private void UpdateHpLabel()
	{
		progressBarHp.Value = hp;
	}

	private void OnPlayerDeath()
	{
		// Player death logic
		GetTree().Quit();
	}

	private void OnInvincibilityTimeout()
	{
		sprite.SelfModulate = new Color(1,1,1,1);
	}

	private void OrderListByDistance()
	{
		enemiesWithinRange.OrderBy(enemy => enemy.GetPosition().DistanceTo(Position)).ToList();
	}

	public List<IEnemy> GetClosestEnemy(int numberOfCloseEnemies)
	{
		/*
		* This method orders the list of enemies by distance and retrieves the specified number of closest enemies.
		* If the number of closest enemies requested exceeds the total number of enemies within range, all enemies within range are returned.
		*/

		OrderListByDistance();
		List<IEnemy> closestEnemies = new List<IEnemy>();

		if (numberOfCloseEnemies > enemiesWithinRange.Count())
		{
			closestEnemies = enemiesWithinRange;

		}
		else
		{
			for (int i = 0; i < numberOfCloseEnemies; i++)
			{
				closestEnemies.Add(enemiesWithinRange[i]);
			}
		}
		return closestEnemies;
	}

	private void OnAttackRangeEntered(Node2D body)
	{
		if (body is IEnemy enemy)
		{
			enemiesWithinRange.Add(enemy);
		}
	}

	private void OnAttackRangeExited(Node2D body)
	{
		if (body is IEnemy enemy)
		{
			enemiesWithinRange.Remove(enemy);
			OrderListByDistance();
		}
	}
}
